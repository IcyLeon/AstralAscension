using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactEffectFactoryManager
{
    private ArtifactBuffInformation root;
    private ArtifactFamilySO artifactFamilySO;

    public ArtifactEffectFactoryManager(ArtifactFamilySO ArtifactFamilySO)
    {
        artifactFamilySO = ArtifactFamilySO;
        ArtifactBuffPieceStat[] ArtifactBuffPieceStatList = artifactFamilySO.PieceBuffs;

        for (int i = 0; i < GetTotalPieceBuffs(); i++)
        {
            CreateNode(ArtifactBuffPieceStatList[i]);
        }
    }

    public int GetTotalPieceBuffs()
    {
        return artifactFamilySO.PieceBuffs.Length;
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
