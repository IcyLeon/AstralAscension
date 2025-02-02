using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ArtifactMainStatDisplay : ArtifactStatDisplay
{
    protected override ArtifactStatSO GetArtifactStatSO()
    {
        if (artifact.mainStat == null)
            return null;

        return artifact.mainStat.statInfo.ArtifactStatSO;
    }

    protected override string UpdateStatValue()
    {
        if (artifact.mainStat == null)
            return "??";

        ArtifactStatSO artifactStatSO = artifact.mainStat.statInfo.ArtifactStatSO;
        float statsValue = artifact.mainStat.statsValue;
        float statsPercentage = statsValue * 0.01f;


        string StatsValueText = artifact.artifactManagerSO.IsPercentageStat(artifactStatSO)
        ? statsPercentage.ToString("P1")
        : Mathf.Round(statsValue).ToString("N0");

        return StatsValueText;
    }
}
