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
            bool allMatch = false;

            for (int j = 0; j < ERInfoList[i].ElementsMixture.Length; j++)
            {
                ElementsSO ElementsSO = ERInfoList[i].ElementsMixture[j];

                if (!ElementsList.ContainsKey(ElementsSO))
                {
                    allMatch = false;
                    break;
                }

                allMatch = true;
            }

            if (allMatch)
                return ERInfoList[i];

        }

        return null;
    }

    public ElementalReactionSO GetImmuneSO()
    {
        return ImmuneSO;
    }
}
