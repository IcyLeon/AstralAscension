using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class ArtifactBuffPieceStat
{
    [SerializeField] private GameObject BuffFactoryPrefab;
    [field: SerializeField, Range(1, 5)] public int NoOfPiece { get; private set; } = 1;
    [field: SerializeField, TextArea] public string Description { get; private set; }

    public ArtifactEffect CreateArtifactEffect()
    {
        ArtifactEffectFactory artifactEffectFactory = BuffFactoryPrefab.GetComponent<ArtifactEffectFactory>();

        if (artifactEffectFactory == null)
            return null;

        return artifactEffectFactory.CreateArtifactEffect(this);
    }
}

[CreateAssetMenu(fileName = "ArtifactManager", menuName = "ScriptableObjects/ArtifactManager/ArtifactFamilySO")]
public class ArtifactFamilySO : ScriptableObject
{
    [field: SerializeField] public string ArtifactSetName { get; private set; }
    [field: SerializeField] public ArtifactBuffPieceStat[] PieceBuffs { get; private set; }
    [field: SerializeField] public ArtifactManagerSO ArtifactManagerSO { get; private set; }

    public ArtifactFamily CreateArtifactFamily()
    {
        return new ArtifactFamily(this);
    }
}