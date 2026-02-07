using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactBuffInformation
{
    public ArtifactBuffPieceStat buffPieceInfo { get; private set; }
    public ArtifactBuffInformation nextNode { get; private set; }
    public ArtifactBuffInformation parentNode { get; private set; }
    public ArtifactBuffInformation(ArtifactBuffPieceStat ArtifactBuffPieceStat, ArtifactBuffInformation Parent)
    {
        buffPieceInfo = ArtifactBuffPieceStat;
        parentNode = Parent;
    }

    public void AddBuffInformation(ArtifactBuffPieceStat ArtifactBuffPieceStat)
    {
        if (nextNode == null)
        {
            nextNode = new(ArtifactBuffPieceStat, this);
            return;
        }

        nextNode.AddBuffInformation(ArtifactBuffPieceStat);
    }

    public bool IsEnough(int totalAmt)
    {
        return totalAmt >= buffPieceInfo.NoOfPiece;
    }
}
