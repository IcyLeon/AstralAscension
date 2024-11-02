using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static ArtifactManagerSO;

public class ArtifactSubStat : ArtifactStat
{
    private List<ArtifactSubStatsInfo> GetAvailableStatInfoList()
    {
        List<ArtifactSubStatsInfo> statList = new();

        ArtifactSubStatsInfo[] artifactSubStatsInfoList = artifact.artifactManagerSO.SubArtifactStatsInfoList;

        for (int i = 0; i < artifactSubStatsInfoList.Length; i++)
        {
            ArtifactSubStatsInfo artifactStatsInfo = artifactSubStatsInfoList[i];
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

        statsValue += statInfo.GetArtifactStatsValue(artifact.GetRarity()).ArtifactCurveStats.Evaluate(0) * statsMultiplier;
    }

    public ArtifactSubStat(Artifact Artifact) : base(Artifact)
    {
        statInfo = GetArtifactSubStatsInfo(GetAvailableStatInfoList());
        Upgrade();
    }

    private ArtifactStatsInfo GetArtifactSubStatsInfo(List<ArtifactSubStatsInfo> AvailableSubStatList)
    {
        float sumOfWeight = 0;
        foreach (var ArtifactSubStatsInfo in AvailableSubStatList)
        {
            sumOfWeight += ArtifactSubStatsInfo.Weight;
        }

        float randomValue = Random.Range(0, sumOfWeight);
        float cumalativeWeight = 0;

        foreach (var ArtifactSubStatsInfo in AvailableSubStatList)
        {
            if (randomValue < ArtifactSubStatsInfo.Weight + cumalativeWeight)
            {
                return ArtifactSubStatsInfo;
            }

            cumalativeWeight += ArtifactSubStatsInfo.Weight;
        }

        return null;
    }

}