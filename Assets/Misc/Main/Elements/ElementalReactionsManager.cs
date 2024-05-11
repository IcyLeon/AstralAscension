using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum ElementalReactionEnums
{
    OVERLOAD,
    MELT,
    BURN,
    FROZEN,
    ELECTRO_CHARGED,
    VAPORIZED,
    SUPERCONDUCT,
}

public class ElementalReactionsManager : MonoBehaviour
{
    public static ElementalReactionsManager instance { get; private set; }

    [Serializable]
    public class ERInfo
    {
        public ElementsSO[] ERElementsRequirementsSOList;
        public ElementalReactionSO result;
    }

    public abstract class DamageInfo : EventArgs
    {
        public float DamageAmount;
        public List<string> DamageText;
        public Vector3 WorldPosition;
    }
    public class ElementsInfo : DamageInfo
    {
        public IElement source;
        public ElementsSO elementsSO;
        public Vector3 HitPosition;
    }
    public class ElementalReactionInfo : DamageInfo
    {
        public ElementalReactionSO elementalReactionSO;
    }

    public static event EventHandler<ElementsInfo> DamageChanged;
    public static event EventHandler<ElementalReactionInfo> ElementalReactionChanged;

    [field: SerializeField] public Color ImmuneColorText { get; private set; }
    [SerializeField] private ERInfo[] ERInfoList;

    private List<ElementalReaction> ElementalReactionList;

    private static Dictionary<ElementalReactionEnums, System.Func<ElementalReactionSO, ElementalReaction>> ER_Dict = new()
    {
        { ElementalReactionEnums.OVERLOAD, (ElementalReactionSO ERSO) => new Overloaded(ERSO) },
        { ElementalReactionEnums.FROZEN, (ElementalReactionSO ERSO) => new Frozen(ERSO)}
    };

    public ElementsSO[] GetElementsSOList(ElementalReactionSO elementalReactionSO)
    {
        for (int i = 0; i < ERInfoList.Length; i++)
        {
            ERInfo ERInfo = ERInfoList[i];
            if (ERInfo.result == elementalReactionSO)
            {
                return ERInfo.ERElementsRequirementsSOList;
            }
        }

        return null;
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
            ERInfo ERInfo = ERInfoList[i];
            int Counter = 0;

            for (int x = 0; x < ERInfo.ERElementsRequirementsSOList.Length; x++)
            {
                ElementsSO ElementsSO = ERInfo.ERElementsRequirementsSOList[x];

                if (ElementsListCopy.ContainsKey(ElementsSO)) 
                {
                    ElementsListCopy.Remove(ElementsSO);
                    Counter++;
                }
            }

            if (Counter >= ERInfo.ERElementsRequirementsSOList.Length && Counter != 0)
                return ERInfo.result;
        }

        return null;
    }


    private ElementalReaction CreateElementalReaction(IDamageable target, ElementsInfo ElementsInfo)
    {
        if (target.GetSelfInflictElementLists() == null)
            return null;

        ElementalReactionSO ERSO = GetERSO(target.GetSelfInflictElementLists());

        if (ERSO == null)
            return null;

        ElementalReaction ER = ER_Dict[ERSO.ElementalReaction](ERSO);
        ER.Init(ElementsInfo, target);

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
        instance = this;
        ElementalReactionList = new();
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
        elements.OnElementDestroy -= OnDestroyElement;

        if (elements.target == null)
            return;

        elements.target.GetSelfInflictElementLists().Remove(elements.elementsSO);
    }

    private void OnDestroyER(ElementalReaction ER)
    {
        ER.OnERDestroy -= OnDestroyER;
        ElementalReactionList.Remove(ER);
    }

    private Elements GetElements(IDamageable target, ElementsSO ElementsSO)
    {
        if (target == null || target.GetSelfInflictElementLists() == null)
            return null;

        if (target.GetSelfInflictElementLists().TryGetValue(ElementsSO, out Elements e))
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
            Debug.Log(e.elementsSO);
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
            target.GetSelfInflictElementLists().Add(e.elementsSO, NewElement);
        }


        ElementalReaction ER = CreateElementalReaction(target, e);

        if (ER == null)
            return;

        ER.OnERDestroy += OnDestroyER;
        ElementalReactionList.Add(ER);
    }

    private void Update()
    {
        UpdateList();
    }

    private void UpdateList()
    {
        for (int i = 0; i < ElementalReactionList.Count; i++)
        {
            ElementalReactionList[i].Update();
        }
    }
    private void OnDestroy()
    {
        DamageChanged -= ElementalReactionsManager_DamageChanged;
        ElementalReactionChanged -= ElementalReactionsManager_ElementalReactionChanged;
    }
}
