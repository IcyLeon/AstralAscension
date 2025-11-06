using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
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

    [Serializable]
    public class ArtifactStatsInfo
    {
        [field: SerializeField] public ArtifactStatSO ArtifactStatSO { get; private set; }
        [field: SerializeField] public ArtifactStatsValue[] ArtifactStatsValue { get; private set; }
        [field: SerializeField] public float Weight { get; private set; }
        public ArtifactStatsValue GetArtifactStatsValue(ItemRaritySO ItemRaritySO)
        {
            foreach (var statInfo in ArtifactStatsValue)
            {
                if (statInfo.ItemRaritySO == ItemRaritySO)
                    return statInfo;
            }
            return null;
        }
    }

    [Serializable]
    public class ArtifactMainStatsTypeInfo
    {
        [field: SerializeField] public ArtifactStatsInfo[] ArtifactStatsInfo { get; private set; }
        [field: SerializeField] public ItemTypeSO ArtifactTypeSO { get; private set; }
    }


    [Serializable]
    public class ArtifactNumberofStat
    {
        [field: SerializeField] public ItemRaritySO ItemRaritySO { get; private set; }
        [field: SerializeField] public int MinNoOfStats { get; private set; }
        [field: SerializeField] public int MaxNoOfStats { get; private set; }
    }

    [field: SerializeField, Header("Main Stats Information")] public ArtifactMainStatsTypeInfo[] MainArtifactStatsInfoList { get; private set; }
    [field: SerializeField, Header("Sub Stats Information")] public ArtifactStatsInfo[] SubArtifactStatsInfoList { get; private set; }
    [SerializeField] private ArtifactNumberofStat[] ArtifactNumberOfStatList;

    [Header("Chance of Getting Max Stat")]
    [Range(0f, 1f)]
    [SerializeField] private float MaxStatProbabilityRequirement;
    [field: SerializeField, Header("Percentage Type Stats")] public ArtifactStatSO[] ArtifactPercentageTypeStatsSOList { get; private set; }
    
    [field: SerializeField, Header("Artifact Upgrade EXP")] public ItemEXPCostManagerSO ArtifactEXPManagerSO { get; private set; }

    /// <summary>
    /// Get the Artifact Family of the Artifacts.Eg; Thundering Fury
    /// </summary>
    //public ArtifactFamilySO GetArtifactFamilySO(iData iData)
    //{
    //    foreach (var family in ArtifactFamilyList)
    //    {
    //        foreach (var artifact in family.ArtifactSetsList)
    //        {
    //            ArtifactSO artifactSO = iData as ArtifactSO;
    //            if (artifact == artifactSO)
    //                return family;
    //        }
    //    }
    //    return null;
    //}

    public int GetArtifactRandomNumberofSubStat(ItemRaritySO ItemRaritySO)
    {
        ArtifactNumberofStat artifactNumberofStat = GetArtifactNumberofSubStat(ItemRaritySO);

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
    public ArtifactNumberofStat GetArtifactNumberofSubStat(ItemRaritySO ItemRaritySO)
    {
        foreach (var ArtifactNumberOfStat in ArtifactNumberOfStatList)
        {
            if (ArtifactNumberOfStat.ItemRaritySO == ItemRaritySO)
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


    public ArtifactStatsInfo GetRandomStats(List<ArtifactStatsInfo> statsInfos)
    {
        float sumofProbability = 0f;

        List<ArtifactStatsInfo> ArtifactStatsInfoList = new(statsInfos);
        for (int i = 0; i < ArtifactStatsInfoList.Count; i++)
        {
            int tempindex = Random.Range(i, ArtifactStatsInfoList.Count - 1);
            ArtifactStatsInfo temp = ArtifactStatsInfoList[i];
            ArtifactStatsInfoList[i] = ArtifactStatsInfoList[tempindex];
            ArtifactStatsInfoList[tempindex] = temp;
            sumofProbability += ArtifactStatsInfoList[i].Weight;
        }

        float cumalativeProbabilty = 0f;
        float randomValue = Random.Range(0, sumofProbability);
        foreach (var ArtifactStat in ArtifactStatsInfoList)
        {
            if (randomValue < ArtifactStat.Weight + cumalativeProbabilty)
            {
                return ArtifactStat;
            }

            cumalativeProbabilty += ArtifactStat.Weight;
        }

        return null;
    }
}
