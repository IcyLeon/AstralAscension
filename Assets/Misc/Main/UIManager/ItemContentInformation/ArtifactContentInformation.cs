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

    public override void UpdateItemContentInformation(IData IData)
    {
        UpdateArtifactSO(IData);
        gameObject.SetActive(artifactSO != null);

        if (artifactSO == null)
            return;

        UpdateArtifactSetTxt();
        OnArtifactContentChanged?.Invoke();
    }

    private void UpdateArtifactSO(IData IData)
    {
        Artifact artifact = IData as Artifact;

        if (artifact != null)
        {
            artifactSO = artifact.artifactSO;
            return;
        }

        artifactSO = IData as ArtifactSO;
    }

    private void UpdateArtifactSetTxt()
    {
        ArtifactFamilySO artifactFamilySO = artifactSO.ArtifactFamilySO;

        if (artifactFamilySO == null)
            return;

        ArtifactSetTxt.text = artifactFamilySO.ArtifactSetName + ":";
    }

    protected override void Init()
    {
    }
}
