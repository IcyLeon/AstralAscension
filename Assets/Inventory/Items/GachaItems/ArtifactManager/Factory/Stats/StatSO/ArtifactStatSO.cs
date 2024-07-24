using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactStatSO", menuName = "ScriptableObjects/ArtifactManager/ArtifactStatSO")]
public class ArtifactStatSO : ScriptableObject
{
    [field: SerializeField] public string ArtifactStat { get; private set; }
}
