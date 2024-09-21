using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArtifactTwoPieceInfoDisplay : ArtifactPieceInfoDisplay
{
    protected override void UpdateVisual()
    {
        ArtifactPieceTxt.text = "2-Piece Set: " + artifactFamilySO.TwoPieceDescription;
    }
}
