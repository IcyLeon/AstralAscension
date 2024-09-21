using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public abstract class ArtifactPieceInfoDisplay : MonoBehaviour
{
    protected ArtifactFamilySO artifactFamilySO;

    [SerializeField] private Image CheckImage;
    [SerializeField] protected TextMeshProUGUI ArtifactPieceTxt;

    protected abstract void UpdateVisual();

    public void SetArtifactFamilySO(ArtifactFamilySO ArtifactFamilySO)
    {
        artifactFamilySO = ArtifactFamilySO;

        if (artifactFamilySO == null)
            return;

        UpdateVisual();
    }

    public void SetTextColor(Color32 color)
    {
        CheckImage.color = color;
        ArtifactPieceTxt.color = color;
    }
}
