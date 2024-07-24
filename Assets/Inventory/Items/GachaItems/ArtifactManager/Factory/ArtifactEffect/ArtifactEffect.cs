using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArtifactEffect
{
    private ArtifactFamilySO artifactFamilySO;

    public abstract void UpdateTwoSetBuff();
    public abstract void UpdateFourSetBuff();

    public ArtifactEffect(ArtifactFamilySO ArtifactFamilySO)
    {
        artifactFamilySO = ArtifactFamilySO;
    }
}
