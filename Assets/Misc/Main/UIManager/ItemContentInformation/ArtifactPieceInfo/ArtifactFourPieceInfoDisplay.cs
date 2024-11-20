using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArtifactFourPieceInfoDisplay : ArtifactPieceInfoDisplay
{
    protected override void UpdateVisual()
    {
        ArtifactPieceTxt.text = "4-Piece Set: " + artifactFamilySO.FourPieceBuff.Description;
    }
}
