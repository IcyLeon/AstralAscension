using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactEffectFactoryManager
{
    private ArtifactBuffInformation root;

    public ArtifactEffectFactoryManager(ArtifactFamilySO ArtifactFamilySO)
    {
        ArtifactBuffPieceStat[] ArtifactBuffPieceStatList = ArtifactFamilySO.PieceBuffs;

        for (int i = 0; i < ArtifactBuffPieceStatList.Length; i++)
        {
            CreateNode(ArtifactBuffPieceStatList[i]);
        }
    }

    public ArtifactBuffInformation GetNextNode(ArtifactBuffInformation currentNode)
    {
        if (currentNode == null)
        {
            return root;
        }

        return currentNode.nextNode;
    }

    private void CreateNode(ArtifactBuffPieceStat ArtifactBuffPieceStat)
    {
        if (root == null)
        {
            root = new(ArtifactBuffPieceStat, root);
            return;
        }

        root.AddBuffInformation(ArtifactBuffPieceStat);

    }
}
