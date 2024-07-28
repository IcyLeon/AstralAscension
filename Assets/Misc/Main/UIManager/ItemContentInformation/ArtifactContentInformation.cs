using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactContentInformation : ItemContentInformation
{
    private ArtifactSubStatDisplay[] artifactSubStatDisplayList;
    [SerializeField] private ArtifactMainStatDisplay artifactMainStatDisplay;

    protected override void Init()
    {
        if (artifactSubStatDisplayList == null)
            artifactSubStatDisplayList = GetComponentsInChildren<ArtifactSubStatDisplay>(true);
    }

    public override void UpdateItemContentInformation(IItem iItem)
    {
        Init();
        Artifact artifact = iItem as Artifact;

        gameObject.SetActive(artifact != null);

        if (artifactMainStatDisplay != null)
        {
            artifactMainStatDisplay.SetArtifactItem(artifact);
        }

        if (!gameObject.activeSelf)
            return;

        for (int i = 0; i < artifactSubStatDisplayList.Length; i++)
        {
            ArtifactSubStatDisplay artifactSubStatDisplay = artifactSubStatDisplayList[i];
            artifactSubStatDisplay.SetIndex(i);
            artifactSubStatDisplay.SetArtifactItem(artifact);
        }
    }
}
