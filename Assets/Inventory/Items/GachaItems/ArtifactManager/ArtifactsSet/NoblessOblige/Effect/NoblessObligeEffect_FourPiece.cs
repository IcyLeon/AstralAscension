using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NoblessObligeEffect_FourPiece : NoblessObligeEffect
{
    public NoblessObligeEffect_FourPiece(ArtifactFamilySO ArtifactFamilySO) : base(ArtifactFamilySO)
    {

    }

    public override ArtifactEffect Clone()
    {
        return new NoblessObligeEffect_FourPiece(artifactFamilySO);
    }
}
