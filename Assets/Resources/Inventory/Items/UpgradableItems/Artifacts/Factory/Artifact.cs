using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
public class Artifact : UpgradableItems
{
    public ArtifactManagerSO artifactManagerSO { get; private set; }
    public ArtifactMainStat mainStat { get; private set; }
    public Dictionary<ArtifactStatSO, ArtifactSubStat> subStats { get; private set; }
    public ArtifactSO artifactSO { get; private set; }

    public Artifact(IItem iItem) : base(iItem)
    {
        subStats = new();
        GenerateMainStat();
        GenerateRandomSubStat();
    }

    protected override void OnCreateUpgradableItem()
    {
        base.OnCreateUpgradableItem();
        artifactSO = GetIItem() as ArtifactSO;
        artifactManagerSO = artifactSO.ArtifactFamilySO.ArtifactManagerSO;
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
        int randomNoSubStats = artifactManagerSO.GetArtifactRandomNumberofSubStat(GetRaritySO());

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
        return subStats.Count >= artifactManagerSO.GetArtifactNumberofSubStat(GetRaritySO()).MaxNoOfStats;
    }

    protected override void UpgradeItemAction()
    {
        base.UpgradeItemAction();
        mainStat.Upgrade();
        if (level % 4 == 0 && amount != 0)
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
