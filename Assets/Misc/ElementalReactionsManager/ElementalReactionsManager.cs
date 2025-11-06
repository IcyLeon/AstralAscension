using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

[DisallowMultipleComponent]
public class ElementalReactionsManager : MonoBehaviour
{
    public static ElementalReactionsManager instance { get; private set; } 

    [SerializeField] private ElementalReactionsManagerSO ElementalReactionManagerSO;

    private Dictionary<IDamageable, List<ElementalReaction>> ElementalReactionDictionary = new();
    public ElementalReactionMiscEvents elementalReactionMiscEvents { get; private set; }


    // Start is called before the first frame update
    private void Awake()
    {
        instance = this;
        elementalReactionMiscEvents = new();
    }

    private void OnEnable()
    {
        elementalReactionMiscEvents.OnAddElementEvent += OnAddElementToTarget;
        elementalReactionMiscEvents.OnElementDamageEvent += OnElementDamageOnTarget;
        elementalReactionMiscEvents.OnElementDamageEvent += OnElementDamageTextSpawn;
    }

    private void OnDisable()
    {
        elementalReactionMiscEvents.OnAddElementEvent -= OnAddElementToTarget;
        elementalReactionMiscEvents.OnElementDamageEvent -= OnElementDamageOnTarget;
        elementalReactionMiscEvents.OnElementDamageEvent -= OnElementDamageTextSpawn;
    }

    private void OnElementDamageOnTarget(ElementDamageInfoEvent e)
    {
        if (e.target == null || isImmune(e.target, e.elementsInfoSO))
            return;

        e.target.SetCurrentHealth(e.target.GetCurrentHealth() - e.damageAmount);

        elementalReactionMiscEvents.AddElement(e);
    }

    private void OnAddElementToTarget(ElementDamageInfoEvent e)
    {
        ElementsSO elementSO = e.elementsInfoSO as ElementsSO;

        if (elementSO == null || e.target == null)
            return;

        Elements ExistElement = GetElements(e.target, elementSO);

        if (ExistElement != null)
        {
            ExistElement.Reset();
        }
        else
        {
            if (!isImmune(e.target, elementSO))
            {
                ExistElement = elementSO.CreateElements(e.target.GetCharacterDataStat());
                ExistElement.OnElementDestroy += OnDestroyElement;
            }

        }

        ElementalReaction ER = CreateElementalReaction(e.target, e);

        if (ER == null)
            return;

        if (!ElementalReactionDictionary.ContainsKey(e.target))
        {
            List<ElementalReaction> reactions = new() { ER };
            ElementalReactionDictionary.Add(e.target, reactions);
        }
        else
        {
            ElementalReactionDictionary[e.target].Add(ER);
        }
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

        ER.OnERDestroy += OnDestroyER;

        return ER;
    }

    private void OnElementDamageTextSpawn(ElementDamageInfoEvent e)
    {
        string DamageTxt = e.damageAmount.ToString();
        ElementsInfoSO currentElementsInfoSO = e.elementsInfoSO;

        if (isImmune(e.target, currentElementsInfoSO))
        {
            DamageTxt = ElementalReactionManagerSO.GetImmuneSO().DisplayElementalReactionText;
            currentElementsInfoSO = ElementalReactionManagerSO.GetImmuneSO();
        }
        else
        {
            if (e.damageAmount == 0)
                return;
        }

        Vector3 WorldPosition = e.hitPosition;
        if (WorldPosition == default(Vector3))
        {
            WorldPosition = e.target.GetCenterBound();
        }

        DamageMiscEvent.SendDamageInfo(new DamageInfo
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

    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        OnUpdate();
    }

    private void OnUpdate()
    {
        UpdateList();
    }

    private void UpdateList()
    {
        foreach(var ElementalReactionList in ElementalReactionDictionary.Values)
        {
            for (int i = 0; i < ElementalReactionList.Count; i++)
            {
                ElementalReaction ER = ElementalReactionList[i];
                ER.Update();
            }
        }
    }
}
