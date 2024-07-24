using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;
using static ArtifactManagerSO;
using static ArtifactManager;

public class Artifact : GachaItem
{
    public static float ARTIFACT_LEVEL_EVENT = 4;
    public ArtifactMainStat mainStat { get; private set; }
    public Dictionary<ArtifactStatSO, ArtifactSubStat> subStats { get; private set; }

    public Artifact(Rarity Rarity, IItem iItem) : base(Rarity, iItem)
    {
        if (instance == null)
        {
            Debug.LogError("Artifact Manager not found!");
        }

        subStats = new();
        GenerateRandomMainStat();
        GenerateRandomSubStat();
    }

    public override void SetEquip(CharactersSO charactersSO)
    {
        if (charactersSO != null)
        {
            instance.AddArtifacts(charactersSO, this);
        }
        else
        {
            instance.RemoveArtifacts(equipByCharacter, this);
        }

        base.SetEquip(charactersSO);
    }

    private void GenerateRandomMainStat()
    {
        mainStat = new ArtifactMainStat(this);
    }

    private void GenerateRandomSubStat()
    {
        int randomNoSubStats = instance.ArtifactManagerSO.GetArtifactRandomNumberofSubStat(GetItemRarity());

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
        return subStats.Count >= instance.ArtifactManagerSO.GetArtifactNumberofSubStat(GetItemRarity()).MaxNoOfStats;
    }

    protected override void UpgradeItemAction()
    {
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
