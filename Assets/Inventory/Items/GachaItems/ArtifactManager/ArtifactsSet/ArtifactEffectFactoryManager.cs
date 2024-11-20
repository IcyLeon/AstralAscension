using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArtifactFamilySO;

public class ArtifactEffectFactoryManager
{
    private List<ArtifactEffectFactory> artifactEffectFactoryList;
    public ArtifactEffectFactoryManager(ArtifactFamilySO ArtifactFamilySO)
    {
        artifactEffectFactoryList = new()
        {
            ArtifactFamilySO.TwoPieceBuff.GetBuffEffect(),
            ArtifactFamilySO.FourPieceBuff.GetBuffEffect(),
        };
    }

    public ArtifactEffectFactory GetArtifactEffectInformation(int index)
    {
        int actualIndex = Mathf.Clamp(index, 0, artifactEffectFactoryList.Count - 1);

        return artifactEffectFactoryList[actualIndex];
    }

    public ArtifactEffect CreatePieceEffect(int index)
    {
        if (index < 0)
            return null;

        ArtifactEffectFactory factory = GetArtifactEffectInformation(index);

        if (factory == null)
        {
            return null;
        }

        return factory.CreateArtifactEffect();
    }
}
