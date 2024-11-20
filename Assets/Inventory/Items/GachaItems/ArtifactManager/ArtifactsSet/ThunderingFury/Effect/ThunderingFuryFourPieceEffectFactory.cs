using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderingFuryFourPieceEffectFactory : ArtifactEffectFactory
{
    public override ArtifactEffect CreateArtifactEffect()
    {
        return new ThunderingFuryEffect_FourPiece();
    }
}
