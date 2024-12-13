using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ArtifactManagerSO;

public class ArtifactMainStat : ArtifactStat
{
    private ArtifactMainStatsTypeInfo artifactMainStatsTypeInfo;

    public float PreviewMainStat(int level)
    {
        AnimationCurve animationCurve = statInfo.GetArtifactStatsValue(artifact.GetRaritySO()).ArtifactCurveStats;
        Keyframe endKeyFrame = animationCurve[animationCurve.length - 1];
        float endValue = endKeyFrame.value;
        float firstValue = animationCurve[0].value;

        return ((endValue - firstValue) / endKeyFrame.time) * level + firstValue;
    }

    public override void Upgrade()
    {
        statsValue = PreviewMainStat(artifact.GetLevel());
    }

    protected override void InitStatsInfo()
    {
        artifactMainStatsTypeInfo = artifact.artifactManagerSO.GetArtifactMainStatsTypeInfo(artifact.GetIItem().GetTypeSO());
        statInfo = artifact.artifactManagerSO.GetRandomStats(artifactMainStatsTypeInfo.ArtifactStatsInfo.ToList());
    }

    public ArtifactMainStat(Artifact Artifact) : base(Artifact)
    {
    }
}