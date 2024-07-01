using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactManagerSO", menuName = "ScriptableObjects/ArtifactManager/ArtifactManagerSO")]
public class ArtifactManagerSO : ScriptableObject
{
    [Serializable]
    public class ArtifactStatsInfo
    {
        [field: SerializeField] public ArtifactStatsTypeInfo[] ArtifactStatsTypeInfo { get; private set; }
        [field: SerializeField] public ArtifactTypeSO ArtifactTypeSO { get; private set; }
    }


    [Serializable]
    public class ArtifactStatsTypeInfo
    {
        [Serializable]
        public class ArtifactStatsValue
        {
            [field: SerializeField] public AnimationCurve ArtifactCurveStats { get; private set; }
            [field: SerializeField] public Rarity Rarity { get; private set; }
        }

        [field: SerializeField] public ArtifactStatsValue[] ArtifactStatsInfo { get; private set; }
        [field: SerializeField] public ArtifactStatSO ArtifactStatSO { get; private set; }
    }

    [field: SerializeField, Header("All Artifacts Set")] public ArtifactFamilySO[] ArtifactFamilyList { get; private set; }
    [field: SerializeField] public ArtifactStatsInfo[] MainArtifactStatsInfoList { get; private set; }
    [field: SerializeField] public ArtifactStatsInfo[] SubArtifactStatsInfoList { get; private set; }
}
