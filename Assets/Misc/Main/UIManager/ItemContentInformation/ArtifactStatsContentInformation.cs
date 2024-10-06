using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactStatsContentInformation : ItemContentInformation
{
    private ArtifactSubStatDisplay[] artifactSubStatDisplayList;
    [SerializeField] private ArtifactMainStatDisplay artifactMainStatDisplay;

    protected override void Init()
    {
        if (artifactSubStatDisplayList != null)
            return;

        artifactSubStatDisplayList = GetComponentsInChildren<ArtifactSubStatDisplay>(true);
    }

    public override void UpdateItemContentInformation(IItem iItem)
    {
        Init();

        Artifact artifact = iItem as Artifact;

        gameObject.SetActive(artifact != null);

        if (artifact == null)
            return;

        if (artifactMainStatDisplay != null)
        {
            artifactMainStatDisplay.SetArtifactItem(artifact);
        }

        for (int i = 0; i < artifactSubStatDisplayList.Length; i++)
        {
            ArtifactSubStatDisplay artifactSubStatDisplay = artifactSubStatDisplayList[i];
            artifactSubStatDisplay.SetIndex(i);
            artifactSubStatDisplay.SetArtifactItem(artifact);
        }
    }
}
