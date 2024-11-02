using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using static ArtifactManager;

public class Artifact : GachaItem
{
    public ArtifactManagerSO artifactManagerSO { get; private set; }
    public ArtifactMainStat mainStat { get; private set; }
    public Dictionary<ArtifactStatSO, ArtifactSubStat> subStats { get; private set; }

    public Artifact(Rarity rarity, IItem iItem) : base(rarity, iItem)
    {
        if (artifactManagerSO == null)
        {
            Debug.LogError("ArtifactManagerSO not found!");
            return;
        }

        subStats = new();
        GenerateMainStat();
        GenerateRandomSubStat();
    }

    protected override void OnCreateUpgradableItem()
    {
        base.OnCreateUpgradableItem();

        if (instance == null)
        {
            Debug.LogError("Artifact Manager not found!");
            return;
        }

        artifactManagerSO = instance.ArtifactManagerSO;
    }

    protected override ItemEXPCostManagerSO InitItemEXPCostManagerSO()
    {
        return artifactManagerSO.ArtifactEXPManagerSO;
    }

    private void GenerateMainStat()
    {
        mainStat = new ArtifactMainStat(this);
    }

    private void GenerateRandomSubStat()
    {
        int randomNoSubStats = artifactManagerSO.GetArtifactRandomNumberofSubStat(GetRarity());

        for (int i = 0; i < randomNoSubStats; i++)
        {
            CreateSubStats();
        }
    }

    private void CreateSubStats()
    {
        ArtifactSubStat ArtifactSubStat = new ArtifactSubStat(this);
        subStats.Add(ArtifactSubStat.statInfo.ArtifactStatSO, ArtifactSubStat);
    }

    private bool HasMaxedOutSubStats()
    {
        return subStats.Count >= artifactManagerSO.GetArtifactNumberofSubStat(GetRarity()).MaxNoOfStats;
    }

    protected override void UpgradeItemAction()
    {
        base.UpgradeItemAction();
        mainStat.Upgrade();
        if (amount % ARTIFACT_LEVEL_EVENT == 0 && amount != 0)
        {
            if (!HasMaxedOutSubStats())
            {
                CreateSubStats();
                return;
            }
            int randomIndex = Random.Range(0, subStats.Count);
            ArtifactStatSO artifactStatSO = subStats.ElementAt(randomIndex).Key;
            subStats[artifactStatSO].Upgrade();
        }
    }
}
