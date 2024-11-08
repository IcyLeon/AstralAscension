using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(fileName = "ArtifactManagerSO", menuName = "ScriptableObjects/ArtifactManager/ArtifactManagerSO")]
public class ArtifactManagerSO : ScriptableObject
{
    [Serializable]
    public class ArtifactStatsValue
    {
        [field: SerializeField] public AnimationCurve ArtifactCurveStats { get; private set; }
        [field: SerializeField] public ItemRaritySO ItemRaritySO { get; private set; }
    }

    public abstract class ArtifactStatsInfo
    {
        [field: SerializeField] public ArtifactStatSO ArtifactStatSO { get; private set; }
        [field: SerializeField] public ArtifactStatsValue[] ArtifactStatsValue { get; private set; }

        public ArtifactStatsValue GetArtifactStatsValue(Rarity Rarity)
        {
            foreach (var statInfo in ArtifactStatsValue)
            {
                if (statInfo.ItemRaritySO.Rarity == Rarity)
                    return statInfo;
            }
            return null;
        }
    }

    [Serializable]
    public class ArtifactMainStatsTypeInfo
    {
        [field: SerializeField] public ArtifactMainStatsInfo[] ArtifactStatsInfo { get; private set; }
        [field: SerializeField] public ItemTypeSO ArtifactTypeSO { get; private set; }

        public ArtifactMainStatsInfo GetRandomMainStats()
        {
            float sumofProbability = 0f;
            foreach (var ArtifactStat in ArtifactStatsInfo)
            {
                sumofProbability += ArtifactStat.ProbabilityRange;
            }

            float cumalativeProbabilty = 0f;
            float randomValue = Random.Range(0, sumofProbability);
            foreach (var ArtifactStat in ArtifactStatsInfo)
            {
                if (randomValue < ArtifactStat.ProbabilityRange + cumalativeProbabilty)
                {
                    return ArtifactStat;
                }

                cumalativeProbabilty += ArtifactStat.ProbabilityRange;
            }

            return null;
        }
    }

    [Serializable]
    public class ArtifactSubStatsInfo : ArtifactStatsInfo
    {
        [field: SerializeField] public float Weight { get; private set; }
    }


    [Serializable]
    public class ArtifactMainStatsInfo : ArtifactStatsInfo
    {
        [field: SerializeField, Range(0, 1)] public float ProbabilityRange { get; private set; }
    }


    [Serializable]
    public class ArtifactNumberofStat
    {
        [field: SerializeField] public ItemRaritySO ItemRaritySO { get; private set; }
        [field: SerializeField] public int MinNoOfStats { get; private set; }
        [field: SerializeField] public int MaxNoOfStats { get; private set; }
    }

    [field: SerializeField, Header("All Artifacts Set")] public ArtifactFamilySO[] ArtifactFamilyList { get; private set; }
    [field: SerializeField, Header("Main Stats Information")] public ArtifactMainStatsTypeInfo[] MainArtifactStatsInfoList { get; private set; }
    [field: SerializeField, Header("Sub Stats Information")] public ArtifactSubStatsInfo[] SubArtifactStatsInfoList { get; private set; }
    [SerializeField] private ArtifactNumberofStat[] ArtifactNumberOfStatList;

    [Header("Chance of Getting Max Stat")]
    [Range(0f, 1f)]
    [SerializeField] private float MaxStatProbabilityRequirement;
    [field: SerializeField, Header("Percentage Type Stats")] public ArtifactStatSO[] ArtifactPercentageTypeStatsSOList { get; private set; }
    
    [field: SerializeField, Header("Artifact Upgrade EXP")] public ItemEXPCostManagerSO ArtifactEXPManagerSO { get; private set; }

    /// <summary>
    /// Get the Artifact Family of the Artifacts. Eg; Thundering Fury
    /// </summary>
    //public ArtifactFamilySO GetArtifactFamilySO(IItem iItem)
    //{
    //    foreach(var family in ArtifactFamilyList)
    //    {
    //        foreach(var artifact in family.ArtifactSetsList)
    //        {
    //            ArtifactSO artifactSO = iItem as ArtifactSO;
    //            if (artifact == artifactSO)
    //                return family;
    //        }
    //    }
    //    return null;
    //}

    public int GetArtifactRandomNumberofSubStat(Rarity Rarity)
    {
        ArtifactNumberofStat artifactNumberofStat = GetArtifactNumberofSubStat(Rarity);

        if (artifactNumberofStat == null)
            return 1;

        float randomValue = Random.value;
        int noOfStats = artifactNumberofStat.MinNoOfStats;

        if (randomValue > MaxStatProbabilityRequirement)
        {
            noOfStats = artifactNumberofStat.MaxNoOfStats;
        }

        return noOfStats;
    }
    public ArtifactNumberofStat GetArtifactNumberofSubStat(Rarity Rarity)
    {
        foreach (var ArtifactNumberOfStat in ArtifactNumberOfStatList)
        {
            if (ArtifactNumberOfStat.ItemRaritySO.Rarity == Rarity)
                return ArtifactNumberOfStat;
        }
        return null;
    }

    public bool IsPercentageStat(ArtifactStatSO artifactStatSO)
    {
        foreach (var ArtifactStatSO in ArtifactPercentageTypeStatsSOList)
        {
            if (artifactStatSO == ArtifactStatSO)
                return true;
        }
        return false;
    }

    public ArtifactMainStatsTypeInfo GetArtifactMainStatsTypeInfo(ItemTypeSO ArtifactTypeSO)
    {
        foreach (var ArtifactStatsInfo in MainArtifactStatsInfoList)
        {
            if (ArtifactStatsInfo.ArtifactTypeSO == ArtifactTypeSO)
                return ArtifactStatsInfo;
        }
        return null;
    }
}
