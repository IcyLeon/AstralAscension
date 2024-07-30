using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactManager", menuName = "ScriptableObjects/ArtifactManager/ArtifactFamilySO")]
public class ArtifactFamilySO : ScriptableObject
{
    [field: SerializeField] public string ArtifactSetName { get; private set; }
    [SerializeField] private ArtifactTypeEnums ArtifactType;
    [field: SerializeField] public ArtifactSO[] ArtifactSetsList { get; private set; }

    [field: SerializeField, TextArea] public string TwoPieceDescription { get; private set; }
    [field: SerializeField, TextArea] public string FourPieceDescription { get; private set; }

    public ArtifactEffect GetArtifactEffect(ArtifactTypeEnums ArtifactTypeEnums)
    {
        ArtifactEffectFactory artifactEffectFactory = ArtifactEffectFactoryManager.CreateArtifactEffectFactory(ArtifactTypeEnums);
        
        if (artifactEffectFactory == null)
            return null;

        ArtifactEffect ArtifactEffect = artifactEffectFactory.CreateArtifactEffect(this);

        return ArtifactEffect;
    }
}