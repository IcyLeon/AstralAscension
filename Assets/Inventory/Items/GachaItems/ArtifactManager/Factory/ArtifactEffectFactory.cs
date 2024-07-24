using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ArtifactTypeEnums
{
    THUNDERING_FURY,
    NOBLESS_OBLIGE
}

public static class ArtifactEffectFactoryManager
{
    private static Dictionary<ArtifactTypeEnums, System.Func<ArtifactEffectFactory>> ArtifactEffectDict = new()
    {
        { ArtifactTypeEnums.THUNDERING_FURY, () => new ThunderingFuryEffectFactory() },
        { ArtifactTypeEnums.NOBLESS_OBLIGE, () => new NoblessObligeEffectFactory() },
    };

    public static ArtifactEffectFactory CreateArtifactEffectFactory(ArtifactTypeEnums a)
    {
        if (!ArtifactEffectDict.ContainsKey(a))
            return null;

        return ArtifactEffectDict[a]();
    }
}

public abstract class ArtifactEffectFactory
{
    public abstract ArtifactEffect CreateArtifactEffect(ArtifactFamilySO ArtifactFamilySO);
}

public class ThunderingFuryEffectFactory : ArtifactEffectFactory
{
    public override ArtifactEffect CreateArtifactEffect(ArtifactFamilySO ArtifactFamilySO)
    {
        return new ThunderingFuryEffect(ArtifactFamilySO);
    }
}

public class NoblessObligeEffectFactory : ArtifactEffectFactory
{
    public override ArtifactEffect CreateArtifactEffect(ArtifactFamilySO ArtifactFamilySO)
    {
        return new NoblessObligeEffect(ArtifactFamilySO);
    }
}