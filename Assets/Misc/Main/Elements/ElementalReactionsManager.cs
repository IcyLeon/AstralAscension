using System;
using System.Collections;
using System.Collections.Generic;
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

public enum ElementsEnums
{
    PYRO,
    CRYO,
    DENDRO,
    ELECTRO,
    HYDRO,
}


public class ElementalReactionsManager : MonoBehaviour
{
    [Serializable]
    public class ERInfo
    {
        public ElementsSO[] ERElementsRequirementsSOList;
        public ElementalReactionSO result;
    }

    [SerializeField] private ElementsSO[] ElementsSOList;
    [SerializeField] private ERInfo[] ERInfoList;

    private Dictionary<ElementalReactionEnums, System.Func<ElementalReaction>> ER_Dict = new Dictionary<ElementalReactionEnums, System.Func<ElementalReaction>>
    {
        { ElementalReactionEnums.OVERLOAD, () => new Overloaded() }
    };

    private ElementalReactionSO GetERSO(List<Elements> ElementsList)
    {
        for (int i = 0; i < ERInfoList.Length; i++)
        {
            List<Elements> ElementsListCopy = new(ElementsList);
            ERInfo ERInfo = ERInfoList[i];
            int Counter = 0;

            for (int x = 0; x < ERInfo.ERElementsRequirementsSOList.Length; x++)
            {
                if (ElementsList.Count != ERInfo.ERElementsRequirementsSOList.Length)
                {
                    break;
                }

                for (int y = 0; y < ElementsListCopy.Count; y++)
                {
                    ElementsSO ElementsSO = ERInfo.ERElementsRequirementsSOList[x];
                    if (ElementsSO.ElementsEnums == ElementsListCopy[y].elementsSO.ElementsEnums)
                    {
                        ElementsListCopy.RemoveAt(y);
                        Counter++;
                    }
                }
            }

            if (Counter == ERInfo.ERElementsRequirementsSOList.Length && Counter != 0)
                return ERInfo.result;
        }

        return null;
    }

    private ElementsSO GetElementsSO(ElementsEnums elementsEnums)
    {
        for (int i = 0; i < ElementsSOList.Length; i++)
        {
            ElementsSO e = ElementsSOList[i];
            if (e.ElementsEnums == elementsEnums)
                return e;
        }
        return null;
    }

    public Elements CreateElements(ElementsEnums elementsEnums)
    {
        ElementsSO ElementsSO = GetElementsSO(elementsEnums);

        if (ElementsSO == null)
            return null;

        return ElementsSO.CreateElements();
    }

    public ElementalReaction CreateElementalReaction(List<Elements> ElementLists)
    {
        ElementalReactionSO ERSO = GetERSO(ElementLists);

        if (ERSO == null)
            return null;

        ElementalReaction ER = ER_Dict[ERSO.ElementalReaction]();
        ER.SetElementalReactionSO(ERSO);
        return ER;
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
