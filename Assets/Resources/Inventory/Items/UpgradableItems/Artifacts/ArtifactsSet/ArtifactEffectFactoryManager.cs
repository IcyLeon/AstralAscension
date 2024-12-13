using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArtifactFamilySO;

public class ArtifactEffectFactoryManager
{
    private ArtifactBuffInformation head;
    private ArtifactBuffInformation tail;

    public ArtifactEffectFactoryManager(ArtifactFamilySO ArtifactFamilySO)
    {
        ArtifactBuffPieceStat[] ArtifactBuffPieceStatList = ArtifactFamilySO.PieceBuffs;

        for (int i = 0; i < ArtifactBuffPieceStatList.Length; i++)
        {
            CreateNode(ArtifactBuffPieceStatList[i]);
        }
    }

    public ArtifactBuffInformation GetStartingNode()
    {
        return head;
    }

    private void CreateNode(ArtifactBuffPieceStat ArtifactBuffPieceStat)
    {
        ArtifactBuffInformation newNode = new(ArtifactBuffPieceStat);

        if (head == null)
        {
            head = tail = newNode;
            return;
        }

        tail.SetNextNode(newNode);
        newNode.SetPrevNode(tail);

        tail = newNode;
    }
}
