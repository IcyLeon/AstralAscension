using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ArtifactManagerSO;

public class ArtifactSubStat : ArtifactStat
{
    private List<ArtifactStatsInfo> GetAvailableStatInfoList()
    {
        List<ArtifactStatsInfo> statList = new();

        ArtifactStatsInfo[] artifactSubStatsInfoList = artifact.artifactManagerSO.SubArtifactStatsInfoList;

        for (int i = 0; i < artifactSubStatsInfoList.Length; i++)
        {
            ArtifactStatsInfo artifactStatsInfo = artifactSubStatsInfoList[i];
            if (!artifact.subStats.ContainsKey(artifactStatsInfo.ArtifactStatSO))
            {
                if (artifact.mainStat.statInfo.ArtifactStatSO == artifactStatsInfo.ArtifactStatSO)
                    continue;

                statList.Add(artifactStatsInfo);
            }
        }
        return statList;
    }

    public override void Upgrade()
    {
        if (statInfo == null)
            return;

        int randomIndex = Random.Range(0, 4);
        float statsMultiplier = 1f - (0.1f * randomIndex);

        statsValue += statInfo.GetArtifactStatsValue(artifact.GetRaritySO()).ArtifactCurveStats.Evaluate(0) * statsMultiplier;
    }

    public ArtifactSubStat(Artifact Artifact) : base(Artifact)
    {
        statInfo = artifact.artifactManagerSO.GetRandomStats(GetAvailableStatInfoList());
        Upgrade();
    }

}