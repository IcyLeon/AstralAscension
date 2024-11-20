using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoblessObligeFourPieceEffectFactory : ArtifactEffectFactory
{
    public override ArtifactEffect CreateArtifactEffect()
    {
        return new NoblessObligeEffect_FourPiece();
    }
}
