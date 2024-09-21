using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderingFuryEffectPieceFactory : ArtifactEffectPieceFactory
{
    public ThunderingFuryEffectPieceFactory(ArtifactFamilySO ArtifactFamilySO) : base(ArtifactFamilySO)
    {
    }

    protected override ArtifactEffect CreateTwoPieceEffect()
    {
        return new ThunderingFuryEffect_TwoPiece(artifactFamilySO);
    }

    protected override ArtifactEffect CreateFourPieceEffect()
    {
        return new ThunderingFuryEffect_FourPiece(artifactFamilySO);
    }
}
