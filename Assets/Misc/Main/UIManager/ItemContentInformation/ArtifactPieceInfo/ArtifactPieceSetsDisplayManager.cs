using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ArtifactPieceSetsDisplayManager : MonoBehaviour
{
    private ObjectPool<ArtifactPieceInfoDisplay> pieceInfoPool;
    [SerializeField] private GameObject ArtifactPieceInfoPrefab;
    private ArtifactContentInformation ArtifactContentInformation;

    private void Awake()
    {
        Init();
    }

    private void ArtifactContentInformation_OnArtifactContentChanged()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        Init();

        pieceInfoPool.ResetAll();

        ArtifactSO artifactSO = ArtifactContentInformation.artifactSO;

        if (artifactSO == null)
            return;

        foreach (var pieceStat in ArtifactContentInformation.artifactSO.ArtifactFamilySO.PieceBuffs)
        {
            SetArtifactBuffPieceStat(pieceStat);
        }
    }

    public int GetTotalPieceInfo()
    {
        return pieceInfoPool.GetTotalAmount();
    }

    private void SetArtifactBuffPieceStat(ArtifactBuffPieceStat ArtifactBuffPieceStat)
    {
        ArtifactPieceInfoDisplay ArtifactPieceInfoDisplay = pieceInfoPool.GetPooledObject();

        if (ArtifactPieceInfoDisplay == null)
            return;

        ArtifactPieceInfoDisplay.SetArtifactBuffPieceStat(ArtifactBuffPieceStat);
    }

    public ArtifactPieceInfoDisplay GetArtifactPieceInfoDisplay(int index)
    {
        return pieceInfoPool.At(index);
    }

    private void Init()
    {
        if (pieceInfoPool != null)
            return;

        ArtifactContentInformation = GetComponentInParent<ArtifactContentInformation>();
        ArtifactContentInformation.OnArtifactContentChanged += ArtifactContentInformation_OnArtifactContentChanged;
        pieceInfoPool = new ObjectPool<ArtifactPieceInfoDisplay>(ArtifactPieceInfoPrefab, transform, 5);
        UpdateVisual();
    }
}
