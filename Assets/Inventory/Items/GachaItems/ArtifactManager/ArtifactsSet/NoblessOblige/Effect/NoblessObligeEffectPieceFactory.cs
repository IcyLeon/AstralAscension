using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoblessObligeEffectPieceFactory : ArtifactEffectPieceFactory
{
    public NoblessObligeEffectPieceFactory(ArtifactFamilySO ArtifactFamilySO) : base(ArtifactFamilySO)
    {
    }

    protected override ArtifactEffect CreateTwoPieceEffect()
    {
        return new NoblessObligeEffect_TwoPiece(artifactFamilySO);
    }

    protected override ArtifactEffect CreateFourPieceEffect()
    {
        return new NoblessObligeEffect_FourPiece(artifactFamilySO);
    }
}
