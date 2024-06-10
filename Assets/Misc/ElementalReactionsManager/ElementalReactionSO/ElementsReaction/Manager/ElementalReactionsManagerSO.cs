using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementalReactionsManagerSO", menuName = "ScriptableObjects/ElementManager/ElementalReactionsManagerSO")]
public class ElementalReactionsManagerSO : ScriptableObject
{
    [SerializeField] private ElementalReactionSO ImmuneSO;
    [SerializeField] private ElementalReactionSO[] ERInfoList;
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

    public ElementalReactionSO GetImmuneSO()
    {
        return ImmuneSO;
    }
}
