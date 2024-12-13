using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactBuffInformation
{
    public ArtifactBuffPieceStat buffPieceInfo { get; private set; }
    public ArtifactBuffInformation nextNode { get; private set; }
    public ArtifactBuffInformation prevNode { get; private set; }
    public ArtifactBuffInformation(ArtifactBuffPieceStat ArtifactBuffPieceStat)
    {
        buffPieceInfo = ArtifactBuffPieceStat;
    }

    public void SetNextNode(ArtifactBuffInformation NextNode)
    {
        nextNode = NextNode;
    }

    public void SetPrevNode(ArtifactBuffInformation PrevNode)
    {
        prevNode = PrevNode;
    }
}
