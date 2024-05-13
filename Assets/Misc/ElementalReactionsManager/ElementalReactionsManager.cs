using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

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

    public abstract class DamageInfo : EventArgs
    {
        public float DamageAmount;
        public List<string> DamageText;
        public Vector3 WorldPosition;
    }
    public class ElementsInfo : DamageInfo
    {
        public IAttacker source;
        public ElementsSO elementsSO;
        public Vector3 HitPosition;
    }
    public class ElementalReactionInfo : DamageInfo
    {
        public ElementalReactionSO elementalReactionSO;
    }

    public static event EventHandler<ElementsInfo> DamageChanged;
    public static event EventHandler<ElementalReactionInfo> ElementalReactionChanged;

    [field: SerializeField] public ElementalReactionSO ImmuneSO { get; private set; }
    [SerializeField] private ElementalReactionSO[] ERInfoList;

    private Dictionary<IDamageable, List<ElementalReaction>> ElementalReactionDictionary;

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

            for (int x = 0; x < ERInfo.ElementsMixture.Length; x++)
            {
                ElementsSO ElementsSO = ERInfo.ElementsMixture[x];

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


    //private ElementalReaction GetExistingER(IDamageable target, ElementalReactionSO ERSO)
    //{
    //    if (target == null)
    //        return null;

    //    if (ElementalReactionDictionary.TryGetValue(target, out List<ElementalReaction> ER))
    //    {
    //        for (int i = 0; i < ER.Count; i++)
    //        {
    //            ElementalReaction e = ER.ElementAt(i);
    //            if (e.elementalReactionSO == ERSO)
    //                return e;
    //        }
    //    }

    //    return null;
    //}

    private ElementalReaction CreateElementalReaction(IDamageable target, ElementsInfo ElementsInfo)
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

    public static void CallDamageInvoke(object sender, ElementsInfo e)
    {
        DamageChanged?.Invoke(sender, e);
    }
    public static void CallElementalReactionDamageInvoke(object sender, ElementalReactionInfo e)
    {
        ElementalReactionChanged?.Invoke(sender, e);
    }


    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        ElementalReactionDictionary = new();
        DamageChanged += ElementalReactionsManager_DamageChanged;
        ElementalReactionChanged += ElementalReactionsManager_ElementalReactionChanged;
    }

    private void ElementalReactionsManager_ElementalReactionChanged(object sender, ElementalReactionInfo e)
    {
        IDamageable target = sender as IDamageable;
        target.SetCurrentHealth(target.GetCurrentHealth() - e.DamageAmount);
    }

    private void OnDestroyElement(Elements elements)
    {
        elements.DestroyEvents();

        if (elements.target == null)
            return;

        elements.target.GetInflictElementLists().Remove(elements.elementsSO);
    }

    private void OnDestroyER(ElementalReaction ER)
    {
        ER.DestroyEvents();

        ElementalReactionDictionary[ER.target].Remove(ER);
    }

    private Elements GetElements(IDamageable target, ElementsSO ElementsSO)
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
    private void ElementalReactionsManager_DamageChanged(object sender, ElementsInfo e)
    {
        IDamageable target = sender as IDamageable;

        if (!isImmune(target, e.elementsSO))
        {
            target.SetCurrentHealth(target.GetCurrentHealth() - e.DamageAmount);
        }

        if (e.elementsSO == null)
            return;

        Elements ExistElement = GetElements(target, e.elementsSO);

        if (ExistElement != null)
        {
            ExistElement.Reset();
        }
        else
        {
            Elements NewElement = e.elementsSO.CreateElements(target);
            NewElement.OnElementDestroy += OnDestroyElement;
            target.GetInflictElementLists().Add(e.elementsSO, NewElement);
        }


        ElementalReaction ER = CreateElementalReaction(target, e);

        if (ER == null)
            return;

        if (!ElementalReactionDictionary.ContainsKey(target))
        {
            List<ElementalReaction> reactions = new();
            reactions.Add(ER);
            ElementalReactionDictionary.Add(target, reactions);
        }
        else
        {
            if (!ElementalReactionDictionary[target].Contains(ER))
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
        DamageChanged -= ElementalReactionsManager_DamageChanged;
        ElementalReactionChanged -= ElementalReactionsManager_ElementalReactionChanged;
    }
}
