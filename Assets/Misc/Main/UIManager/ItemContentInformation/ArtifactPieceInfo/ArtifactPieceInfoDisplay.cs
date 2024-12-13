using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class ArtifactPieceInfoDisplay : MonoBehaviour
{
    private ArtifactBuffPieceStat artifactBuffPieceStat;

    [SerializeField] private Image CheckImage;
    [SerializeField] protected TextMeshProUGUI ArtifactPieceTxt;

    private void UpdateVisual()
    {
        ArtifactPieceTxt.text = artifactBuffPieceStat.NoOfPiece + "-Piece Set: " + artifactBuffPieceStat.Description;
    }

    public void SetArtifactBuffPieceStat(ArtifactBuffPieceStat ArtifactBuffPieceStat)
    {
        artifactBuffPieceStat = ArtifactBuffPieceStat;
        UpdateVisual();
    }


    public void SetTextColor(Color32 color)
    {
        CheckImage.color = color;
        ArtifactPieceTxt.color = color;
    }
}
