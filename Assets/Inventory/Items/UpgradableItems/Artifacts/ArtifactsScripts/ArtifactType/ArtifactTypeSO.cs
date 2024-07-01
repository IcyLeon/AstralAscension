using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactTypeSO", menuName = "ScriptableObjects/ArtifactManager/ArtifactTypeSO")]
public class ArtifactTypeSO : ScriptableObject
{
    [field: SerializeField] public string ArtifactTypeName { get; private set; }
}
