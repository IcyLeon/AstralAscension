using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ArtifactSubStatDisplay : ArtifactStatDisplay
{
    private int index;

    /// <summary>
    /// This function comes FIRST before setting the item interface reference
    /// </summary>
    public void SetIndex(int i)
    {
        index = i;
    }

    public override void UpdateStatDisplayVisiblity(Artifact Artifact)
    {
        gameObject.SetActive(Artifact != null && !IsOutOfRange(Artifact));
    }

    private bool IsOutOfRange(Artifact Artifact)
    {
        return index >= Artifact.subStats.Count;
    }

    protected override ArtifactStatSO GetArtifactStatSO()
    {
        if (IsOutOfRange(artifact))
            return null;

        return artifact.subStats.ElementAt(index).Key;
    }

    protected string GetRawStatValueDisplay()
    {
        ArtifactStatSO artifactStatSO = GetArtifactStatSO();

        if (IsOutOfRange(artifact) || artifactStatSO == null)
            return "";

        float statsValue = artifact.subStats[artifactStatSO].statsValue;
        float statsPercentage = statsValue * 0.01f;

        string StatsValueText = artifact.artifactManagerSO.IsPercentageStat(artifactStatSO)
        ? statsPercentage.ToString("P1")
        : Mathf.Round(statsValue).ToString("N0");

        return StatsValueText;
    }

    protected override string UpdateStatValue()
    {
        return "+" + GetRawStatValueDisplay();
    }
}
