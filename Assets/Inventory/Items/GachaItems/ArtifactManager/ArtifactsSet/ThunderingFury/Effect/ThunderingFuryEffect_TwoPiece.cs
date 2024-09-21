using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderingFuryEffect_TwoPiece : ThunderingFuryEffect
{
    public ThunderingFuryEffect_TwoPiece(ArtifactFamilySO ArtifactFamilySO) : base(ArtifactFamilySO)
    {

    }

    public override ArtifactEffect Clone()
    {
        return new ThunderingFuryEffect_TwoPiece(artifactFamilySO);
    }
}
