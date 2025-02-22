using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscensionInfomationFactory
{
    private AscensionInformation root;

    public AscensionInfomationFactory(AscensionSO AscensionSO)
    {
        CreateAscensionsData(AscensionSO);
    }

    public AscensionInformation GetNextAscensionNode(AscensionInformation currentNode)
    {
        if (currentNode == null)
        {
            return root;
        }

        return currentNode.nextNode;
    }


    private void CreateAscensionsData(AscensionSO AscensionSO)
    {
        for (int i = 0; i < AscensionSO.AscensionInfoStat.Length; i++)
        {
            AscensionInfoStat AscensionInfoStat = AscensionSO.AscensionInfoStat[i];
            CreateNode(AscensionInfoStat);
        }
    }

    private void CreateNode(AscensionInfoStat AscensionInfoStat)
    {
        if (root == null)
        {
            root = new AscensionInformation(AscensionInfoStat.CreateAscension(), root);
            return;
        }

        root.CreateNode(AscensionInfoStat);
    }
}
