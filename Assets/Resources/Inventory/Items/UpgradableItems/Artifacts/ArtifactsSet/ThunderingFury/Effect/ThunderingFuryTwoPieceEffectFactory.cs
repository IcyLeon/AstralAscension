using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderingFuryTwoPieceEffectFactory : ArtifactEffectFactory
{
    public override ArtifactEffect CreateArtifactEffect(ArtifactBuffPieceStat ArtifactBuffPieceStat)
    {
        return new ThunderingFuryEffect_TwoPiece();
    }
}
