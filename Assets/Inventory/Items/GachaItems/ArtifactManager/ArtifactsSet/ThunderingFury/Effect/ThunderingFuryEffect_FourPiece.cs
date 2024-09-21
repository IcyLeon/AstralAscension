using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThunderingFuryEffect_FourPiece : ThunderingFuryEffect
{
    public ThunderingFuryEffect_FourPiece(ArtifactFamilySO ArtifactFamilySO) : base(ArtifactFamilySO)
    {

    }

    public override ArtifactEffect Clone()
    {
        return new ThunderingFuryEffect_FourPiece(artifactFamilySO);
    }
}
