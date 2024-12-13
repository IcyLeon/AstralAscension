using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoblessObligeTwoPieceEffectFactory : ArtifactEffectFactory
{
    public override ArtifactEffect CreateArtifactEffect(ArtifactBuffPieceStat ArtifactBuffPieceStat)
    {
        return new NoblessObligeEffect_TwoPiece();
    }
}
