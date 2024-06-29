using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactsManagerSO", menuName = "ScriptableObjects/ArtifactsManager/ArtifactsManagerSO")]
public class ArtifactsManagerSO : ScriptableObject
{
    [System.Serializable]
    public class ArtifactTypeInfo
    {
        public string ArtifactTypeName;
        public ArtifactsType ArtifactsType;
    }

    [field: SerializeField, Header("All Artifacts Set")] public ArtifactsFamilySO[] ArtifactsFamilyList { get; private set; }
    [field: SerializeField, Header("Artifact Type Information")] public ArtifactTypeInfo[] ArtifactTypeInfoList { get; private set; }

    public string GetArtifactTypeName(ArtifactsType artifactsType)
    {
        foreach(var artifactTypeInfo in ArtifactTypeInfoList)
        {
            if (artifactTypeInfo.ArtifactsType == artifactsType)
                return artifactTypeInfo.ArtifactTypeName;
        }

        return "???";
    }
}
