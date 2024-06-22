using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactsFamilySO", menuName = "ScriptableObjects/ArtifactsManager/ArtifactsFamilySO")]
public class ArtifactsFamilySO : ScriptableObject
{
    [field: SerializeField] public ArtifactsSO[] ArtifactsSOList { get; private set; }
}
