using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ArtifactEffectFactory : MonoBehaviour
{
    public abstract ArtifactEffect CreateArtifactEffect();
}
