using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ArtifactContentInformation : ItemContentInformation
{
    [SerializeField] private TextMeshProUGUI ArtifactSetTxt;
    [SerializeField] private ArtifactPieceSetsDisplayManager ArtifactPieceSetsDisplayManager;

    public override void UpdateItemContentInformation(IItem iItem)
    {
        ArtifactSO artifactSO = iItem.GetIItem() as ArtifactSO;

        gameObject.SetActive(artifactSO != null);

        if (artifactSO == null)
            return;

        ArtifactFamilySO artifactFamilySO = artifactSO.ArtifactFamilySO;

        if (artifactFamilySO == null)
            return;

        ArtifactSetTxt.text = artifactFamilySO.ArtifactSetName + ":";

        ArtifactPieceSetsDisplayManager.SetArtifactFamilySO(artifactFamilySO);
    }

    protected override void Init()
    {
    }
}
