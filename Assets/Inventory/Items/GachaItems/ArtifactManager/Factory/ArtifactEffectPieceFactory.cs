using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public abstract class ArtifactEffectPieceFactory
{
    private List<ArtifactEffect> artifactEffectList; // only to store information, NOT active effects

    protected ArtifactFamilySO artifactFamilySO;
    protected abstract ArtifactEffect CreateTwoPieceEffect();
    protected abstract ArtifactEffect CreateFourPieceEffect();

    public ArtifactEffectPieceFactory(ArtifactFamilySO ArtifactFamilySO)
    {
        artifactFamilySO = ArtifactFamilySO;

        artifactEffectList = new()
        {
            CreateTwoPieceEffect(),
            CreateFourPieceEffect()
        };
    }

    public ArtifactEffect GetArtifactEffectInformation(int index)
    {
        int actualIndex = Mathf.Clamp(index, 0, artifactEffectList.Count - 1);

        return artifactEffectList[actualIndex];
    }

    public ArtifactEffect CreatePieceEffect(int index)
    {
        return GetArtifactEffectInformation(index).Clone();
    }
}