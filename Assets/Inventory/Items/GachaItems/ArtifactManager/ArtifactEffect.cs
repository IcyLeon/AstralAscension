using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArtifactEffect : BuffEffect
{
    protected ArtifactFamilySO artifactFamilySO;

    public ArtifactEffect(ArtifactFamilySO ArtifactFamilySO)
    {
        artifactFamilySO = ArtifactFamilySO;
    }

    public abstract ArtifactEffect Clone();

    public ArtifactEffect() : base()
    {

    }
}
