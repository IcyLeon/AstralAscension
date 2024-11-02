using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArtifactManagerSO;

public class ArtifactMainStat : ArtifactStat
{
    private ArtifactMainStatsTypeInfo artifactMainStatsTypeInfo;

    public float PreviewMainStat(int level)
    {
        AnimationCurve animationCurve = statInfo.GetArtifactStatsValue(artifact.GetRarity()).ArtifactCurveStats;
        Keyframe endKeyFrame = animationCurve[animationCurve.length - 1];
        float endValue = endKeyFrame.value;
        float firstValue = animationCurve[0].value;

        return ((endValue - firstValue) / endKeyFrame.time) * level + firstValue;
    }

    public override void Upgrade()
    {
        statsValue = PreviewMainStat(artifact.amount);
    }


    public ArtifactMainStat(Artifact Artifact) : base(Artifact)
    {
        artifactMainStatsTypeInfo = artifact.artifactManagerSO.GetArtifactMainStatsTypeInfo(artifact.GetInterfaceItemReference().GetTypeSO());
        statInfo = artifactMainStatsTypeInfo.GetRandomMainStats();
        Upgrade();
    }
}