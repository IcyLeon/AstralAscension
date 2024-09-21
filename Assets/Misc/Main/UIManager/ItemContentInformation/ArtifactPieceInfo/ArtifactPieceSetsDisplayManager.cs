using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactPieceSetsDisplayManager : MonoBehaviour
{
    public ArtifactPieceInfoDisplay[] ArtifactPieceInfoDisplayList { get; private set; }

    private void Awake()
    {
        Init();
    }

    // Start is called before the first frame update
    public void SetArtifactFamilySO(ArtifactFamilySO ArtifactFamilySO)
    {
        Init();

        foreach (var pieceInfo in ArtifactPieceInfoDisplayList)
        {
            pieceInfo.SetArtifactFamilySO(ArtifactFamilySO);
        }
    }

    private void Init()
    {
        if (ArtifactPieceInfoDisplayList != null)
            return;

        ArtifactPieceInfoDisplayList = GetComponentsInChildren<ArtifactPieceInfoDisplay>();
    }
}
