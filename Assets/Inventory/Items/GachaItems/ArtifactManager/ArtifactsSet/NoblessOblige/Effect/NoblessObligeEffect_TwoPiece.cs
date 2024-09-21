using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoblessObligeEffect_TwoPiece : NoblessObligeEffect
{
    public NoblessObligeEffect_TwoPiece(ArtifactFamilySO ArtifactFamilySO) : base(ArtifactFamilySO)
    {

    }

    public override ArtifactEffect Clone()
    {
        return new NoblessObligeEffect_TwoPiece(artifactFamilySO);
    }
}
