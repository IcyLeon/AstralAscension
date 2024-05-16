using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static DamageManager;

public enum ElementalReactionEnums
{
    NONE,
    OVERLOAD,
    MELT,
    BURN,
    FROZEN,
    ELECTRO_CHARGED,
    VAPORIZED,
    SUPERCONDUCT,
}

[DisallowMultipleComponent]
public class ElementalReactionsManager : MonoBehaviour
{
    public static ElementalReactionsManager instance { get; private set; }

    public abstract class DamageEvent
    {
        public float damageAmount;
    }

    public class ElementDamageInfoEvent : DamageEvent
    {
        public ElementsSO elementsSO;
        public IAttacker source;
        public Vector3 hitPosition;
    }

    public class ElementalReactionDamageInfoEvent : DamageEvent
    {
        public ElementalReactionSO elementalReactionSO;
    }

    public static event EventHandler<ElementDamageInfoEvent> OnEDamageHit;
    public static event EventHandler<ElementalReactionDamageInfoEvent> OnERDamageHit;

    [field: SerializeField] public ElementalReactionSO ImmuneSO { get; private set; }
    [SerializeField] private ElementalReactionSO[] ERInfoList;

    private Dictionary<IDamageable, List<ElementalReaction>> ElementalReactionDictionary = new();

    private static Dictionary<ElementalReactionEnums, System.Func<ElementalReactionFactory>> ER_Dict = new()
    {
        { ElementalReactionEnums.OVERLOAD, () => new OverloadFactory() },
        { ElementalReactionEnums.FROZEN, () => new FrozenFactory() },
        { ElementalReactionEnums.ELECTRO_CHARGED, () => new ElectricChargedFactory() }
    };

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        ElementalReactionDictionary.Clear();
    }

    public bool isImmune(IDamageable target, ElementsSO incomingElementSO)
    {
        if (target == null || target.GetImmuneableElementsSO() == null)
            return false;

        for (int i = 0; i < target.GetImmuneableElementsSO().Length; i++)
        {
            ElementsSO e = target.GetImmuneableElementsSO()[i];
            if (e == incomingElementSO)
                return true;
        }

        return false;
    }

    public ElementalReactionSO GetERSO(Dictionary<ElementsSO, Elements> ElementsList)
    {
        if (ElementsList == null)
            return null;

        for (int i = 0; i < ERInfoList.Length; i++)
        {
            Dictionary<ElementsSO, Elements> ElementsListCopy = new(ElementsList);
            ElementalReactionSO ERInfo = ERInfoList[i];
            int Counter = 0;

            for (int j = 0; j < ERInfo.ElementsMixture.Length; j++)
            {
                ElementsSO ElementsSO = ERInfo.ElementsMixture[j];

                if (ElementsListCopy.ContainsKey(ElementsSO)) 
                {
                    ElementsListCopy.Remove(ElementsSO);
                    Counter++;
                }
            }

            if (Counter >= ERInfo.ElementsMixture.Length && Counter != 0)
                return ERInfo;
        }

        return null;
    }

    private ElementalReaction CreateElementalReaction(IDamageable target, ElementDamageInfoEvent ElementsInfo)
    {
        if (target.GetInflictElementLists() == null || target == null)
            return null;

        ElementalReactionSO ERSO = GetERSO(target.GetInflictElementLists());

        if (ERSO == null)
            return null;

        ElementalReactionFactory factory = ER_Dict[ERSO.ElementalReaction]();
        ElementalReaction ER = factory.CreateER(ERSO, ElementsInfo, target);

        ER.OnERDestroy += OnDestroyER;

        return ER;
    }

    public static void CallDamageInvoke(object sender, ElementDamageInfoEvent e)
    {
        OnEDamageHit?.Invoke(sender, e);
    }

    public static void CallElementalReactionDamageInvoke(object sender, ElementalReactionDamageInfoEvent e)
    {
        OnERDamageHit?.Invoke(sender, e);
    }


    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;

        OnEDamageHit += OnElementDamageHit;
        OnEDamageHit += OnElementDamageTextSpawn;
        OnERDamageHit += OnElementalReactionDamageHit;
    }

    private void OnElementalReactionDamageHit(object sender, ElementalReactionDamageInfoEvent e)
    {
        IDamageable target = sender as IDamageable;

        target.SetCurrentHealth(target.GetCurrentHealth() - e.damageAmount);
    }

    private void OnElementDamageTextSpawn(object sender, ElementDamageInfoEvent e)
    {
        IDamageable target = sender as IDamageable;

        if (target == null)
            return;

        string DamageTxt = e.damageAmount.ToString();
        ElementsInfoSO ElementsInfoSO = e.elementsSO;

        if (isImmune(target, e.elementsSO))
        {
            DamageTxt = ImmuneSO.DisplayElementalReactionText;
            ElementsInfoSO = ImmuneSO;
        }

        CallOnDamageTextInvoke(sender, new DamageInfo
        {
            DamageText = DamageTxt,
            ElementsInfoSO = ElementsInfoSO,
            WorldPosition = target.GetCenterBound()
        });
    }

    private void OnDestroyElement(Elements elements)
    {
        elements.DestroyEvents();

        if (elements.target == null)
            return;

        elements.target.RemoveElement(elements.elementsSO, elements);
    }

    private void OnDestroyER(ElementalReaction ER)
    {
        ER.DestroyEvents();

        ElementalReactionDictionary[ER.target].Remove(ER);
    }

    public static Elements GetElements(IDamageable target, ElementsSO ElementsSO)
    {
        if (target == null || target.GetInflictElementLists() == null)
            return null;

        if (target.GetInflictElementLists().TryGetValue(ElementsSO, out Elements e))
        {
            return e;
        }
        return null;
    } 

    // create elemental reaction
    private void OnElementDamageHit(object sender, ElementDamageInfoEvent e)
    {
        IDamageable target = sender as IDamageable;

        if (isImmune(target, e.elementsSO))
        {
            return;
        }

        target.SetCurrentHealth(target.GetCurrentHealth() - e.damageAmount);

        if (e.elementsSO == null)
            return;

        Elements ExistElement = GetElements(target, e.elementsSO);

        if (ExistElement != null)
        {
            ExistElement.Reset();
        }
        else
        {
            ExistElement = e.elementsSO.CreateElements(target);
            ExistElement.OnElementDestroy += OnDestroyElement;
            target.AddElement(e.elementsSO, ExistElement);

        }


        ElementalReaction ER = CreateElementalReaction(target, e);

        if (ER == null)
            return;

        if (!ElementalReactionDictionary.ContainsKey(target))
        {
            List<ElementalReaction> reactions = new() { ER };
            ElementalReactionDictionary.Add(target, reactions);
        }
        else
        {
            ElementalReactionDictionary[target].Add(ER);
        }

    }

    private void Update()
    {
        UpdateList();
    }

    private void UpdateList()
    {
        for (int i = 0; i < ElementalReactionDictionary.Count; i++)
        {
            for (int j = 0; j < ElementalReactionDictionary.ElementAt(i).Value.Count; j++)
            {
                ElementalReaction ER = ElementalReactionDictionary.ElementAt(i).Value.ElementAt(j);
                ER.Update();
            }
        }
    }
    private void OnDestroy()
    {
        OnEDamageHit -= OnElementDamageHit;
        OnEDamageHit -= OnElementDamageTextSpawn;
        OnERDamageHit -= OnElementalReactionDamageHit;
    }
}
