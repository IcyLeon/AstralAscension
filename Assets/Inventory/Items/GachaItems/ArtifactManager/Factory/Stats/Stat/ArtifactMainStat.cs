using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArtifactManagerSO;

public class ArtifactMainStat : ArtifactStat
{
    private ArtifactMainStatsTypeInfo artifactMainStatsTypeInfo;

    public float PreviewMainStat(int level)
    {
        return statInfo.GetArtifactStatsValue(artifact.GetItemRarity()).ArtifactCurveStats.Evaluate(level);
    }

    public override void Upgrade()
    {
        statsValue = PreviewMainStat(artifact.amount);
    }


    public ArtifactMainStat(Artifact Artifact) : base(Artifact)
    {
        artifactMainStatsTypeInfo = ArtifactManager.instance.ArtifactManagerSO.GetArtifactMainStatsTypeInfo(artifact.GetInterfaceItemReference().GetItemType());
        statInfo = artifactMainStatsTypeInfo.GetRandomMainStats();
        Upgrade();
    }
}