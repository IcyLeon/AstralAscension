using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArtifactContentInformation : ItemContentInformation
{
    [SerializeField] private TextMeshProUGUI ArtifactSetTxt;

    public ArtifactSO artifactSO { get; private set; }
    public event Action OnArtifactContentChanged;

    public override void UpdateItemContentInformation(IItem iItem)
    {
        artifactSO = iItem.GetIItem() as ArtifactSO;

        gameObject.SetActive(artifactSO != null);

        if (artifactSO == null)
            return;

        ArtifactFamilySO artifactFamilySO = artifactSO.ArtifactFamilySO;

        if (artifactFamilySO == null)
            return;

        ArtifactSetTxt.text = artifactFamilySO.ArtifactSetName + ":";

        OnArtifactContentChanged?.Invoke();
    }

    protected override void Init()
    {
    }
}
