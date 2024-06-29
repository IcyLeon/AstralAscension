using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactsFamilySO", menuName = "ScriptableObjects/ArtifactsManager/ArtifactsFamilySO")]
public class ArtifactsFamilySO : ScriptableObject
{
    [field: SerializeField] public ArtifactsSO[] ArtifactSetsList { get; private set; }

    [field: SerializeField, TextArea] public string TwoPieceDescription { get; private set; }
    [field: SerializeField, TextArea] public string FourPieceDescription { get; private set; }

    public ArtifactsSO GetArtifactsSO(ArtifactsType ArtifactsType)
    {
        foreach(var Artifact in ArtifactSetsList)
        {
            if (Artifact.ArtifactsType == ArtifactsType)
                return Artifact;
        }
        return null;
    }
}
