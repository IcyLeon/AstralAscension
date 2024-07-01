using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactSO", menuName = "ScriptableObjects/ArtifactManager/ArtifactSO")]
public class ArtifactSO : ItemSO
{
    [field: SerializeField, Header("Artifact Type")] public ArtifactTypeSO ArtifactTypeSO { get; private set; }
}
