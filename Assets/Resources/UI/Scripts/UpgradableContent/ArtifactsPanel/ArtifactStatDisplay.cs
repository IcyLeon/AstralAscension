using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public abstract class ArtifactStatDisplay : MonoBehaviour
{
    protected Artifact artifact { get; private set; }
    [SerializeField] private TextMeshProUGUI ArtifactStatNameTxt;
    [SerializeField] private TextMeshProUGUI ArtifactStatValueTxt;

    private void UpdateVisual()
    {
        if (artifact == null)
            return;

        ArtifactStatNameTxt.text = UpdateStatName();
        ArtifactStatValueTxt.text = UpdateStatValue();
    }

    protected abstract ArtifactStatSO GetArtifactStatSO();
    private string UpdateStatName()
    {
        ArtifactStatSO artifactStatSO = GetArtifactStatSO();

        if (artifactStatSO == null)
            return "??";

        return artifactStatSO.ArtifactStat;
    }

    protected abstract string UpdateStatValue();

    public virtual void UpdateStatDisplayVisiblity(Artifact Artifact)
    {
        gameObject.SetActive(Artifact != null);
    }

    public void SetArtifactItem(Artifact Artifact)
    {
        UpdateStatDisplayVisiblity(Artifact);
        artifact = Artifact;
        UpdateVisual();
    }
}
