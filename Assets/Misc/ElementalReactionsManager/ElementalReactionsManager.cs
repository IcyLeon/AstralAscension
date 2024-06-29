using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using static DamageManager;
using static UnityEditor.Rendering.FilterWindow;


[DisallowMultipleComponent]
public class ElementalReactionsManager : MonoBehaviour
{
    public class ElementDamageInfoEvent : EventArgs
    {
        public ElementsInfoSO elementsInfoSO;
        public float damageAmount;
        public Vector3 hitPosition;
        public IAttacker source;
    }

    public static ElementalReactionsManager instance { get; private set; } 

    public static event EventHandler<ElementDamageInfoEvent> OnElementDamageEvent;
    public static event EventHandler<ElementDamageInfoEvent> OnAddElementEvent;

    [SerializeField] private ElementalReactionsManagerSO ElementalReactionManagerSO;

    private Dictionary<IDamageable, List<ElementalReaction>> ElementalReactionDictionary = new();

    private void OnEnable()
    {
        SceneManager.sceneLoaded += SceneManager_sceneLoaded;
    }
    private void OnDisable()
    {
        SceneManager.sceneLoaded -= SceneManager_sceneLoaded;
    }

    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;

        OnElementDamageEvent += OnElementDamageHit;
        OnElementDamageEvent += OnElementDamageTextSpawn;

        OnAddElementEvent += OnAddElements;
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        ElementalReactionDictionary.Clear();
    }

    public bool isImmune(IDamageable target, ElementsInfoSO incomingElementIsnfoSO)
    {
        if (target == null || target.GetImmuneableElementsInfoSO() == null)
            return false;

        for (int i = 0; i < target.GetImmuneableElementsInfoSO().Length; i++)
        {
            ElementsInfoSO e = target.GetImmuneableElementsInfoSO()[i];
            if (e == incomingElementIsnfoSO)
                return true;
        }

        return false;
    }


    private ElementalReaction CreateElementalReaction(IDamageable target, ElementDamageInfoEvent ElementsInfo)
    {
        if (target == null || target.GetCharacterDataStat() == null)
            return null;

        ElementalReactionSO ERSO = ElementalReactionManagerSO.GetERSO(target.GetCharacterDataStat().inflictElementList);

        if (ERSO == null)
            return null;

        ElementalReaction ER = ERSO.CreateElementalReaction(target, ElementsInfo);

        if (ER == null)
            return null;

        ER.OnERDestroy += OnDestroyER;

        return ER;
    }

    public static void CallAddElementInvoke(object sender, ElementDamageInfoEvent e)
    {
        OnAddElementEvent?.Invoke(sender, e);
    }

    public static void CallElementDamageInvoke(object sender, ElementDamageInfoEvent e)
    {
        OnElementDamageEvent?.Invoke(sender, e);
    }

    private void OnElementDamageTextSpawn(object sender, ElementDamageInfoEvent e)
    {
        IDamageable target = sender as IDamageable;

        if (target == null)
            return;

        string DamageTxt = e.damageAmount.ToString();
        ElementsInfoSO currentElementsInfoSO = e.elementsInfoSO;

        if (isImmune(target, currentElementsInfoSO))
        {
            DamageTxt = ElementalReactionManagerSO.GetImmuneSO().DisplayElementalReactionText;
            currentElementsInfoSO = ElementalReactionManagerSO.GetImmuneSO();
        }
        else
        {
            if (e.damageAmount == 0)
                return;
        }

        MonoBehaviour transform = target as MonoBehaviour;
        if (transform != null)
        {
            if (!transform.gameObject.activeSelf)
                return;
        }

        Vector3 WorldPosition = e.hitPosition;
        if (WorldPosition == default(Vector3))
        {
            WorldPosition = target.GetCenterBound();
        }

        CallOnDamageTextInvoke(sender, new DamageInfo
        {
            DamageText = DamageTxt,
            ElementsInfoSO = currentElementsInfoSO,
            WorldPosition = WorldPosition
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
        if (target == null || target.GetCharacterDataStat() == null)
            return null;

        if (target.GetCharacterDataStat().inflictElementList != null && target.GetCharacterDataStat().inflictElementList.TryGetValue(ElementsSO, out Elements e))
        {
            return e;
        }
        return null;
    }

    private void OnAddElements(object sender, ElementDamageInfoEvent e)
    {
        IDamageable target = sender as IDamageable;

        ElementsSO elementSO = e.elementsInfoSO as ElementsSO;

        if (elementSO == null ||  target == null)
            return;

        Elements ExistElement = GetElements(target, elementSO);

        if (ExistElement != null)
        {
            ExistElement.Reset();
        }
        else
        {
            if (!isImmune(target, elementSO))
            {
                ExistElement = elementSO.CreateElements(target.GetCharacterDataStat());
                ExistElement.OnElementDestroy += OnDestroyElement;
            }

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

    // create elemental reaction
    private void OnElementDamageHit(object sender, ElementDamageInfoEvent e)
    {
        IDamageable target = sender as IDamageable;

        if (target == null || isImmune(target, e.elementsInfoSO))
            return;

        target.SetCurrentHealth(target.GetCurrentHealth() - e.damageAmount);

        CallAddElementInvoke(target, e);
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
        OnElementDamageEvent -= OnElementDamageHit;
        OnElementDamageEvent -= OnElementDamageTextSpawn;
        OnAddElementEvent -= OnAddElements;
    }
}
