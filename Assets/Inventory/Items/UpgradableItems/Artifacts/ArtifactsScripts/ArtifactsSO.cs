using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArtifactsType
{
    FLOWER,
    FEATHER,
    SANDS,
    GOBLET,
    CIRCLET
}

[CreateAssetMenu(fileName = "ArtifactsSO", menuName = "ScriptableObjects/ArtifactsManager/ArtifactsSO")]
public class ArtifactsSO : ItemSO
{
    [field: SerializeField] public ArtifactsType ArtifactsType { get; private set; }
}
