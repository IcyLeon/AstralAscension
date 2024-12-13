using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoblessObligeFourPieceEffectFactory : ArtifactEffectFactory
{
    public override ArtifactEffect CreateArtifactEffect(ArtifactBuffPieceStat ArtifactBuffPieceStat)
    {
        return new NoblessObligeEffect_FourPiece();
    }
}
