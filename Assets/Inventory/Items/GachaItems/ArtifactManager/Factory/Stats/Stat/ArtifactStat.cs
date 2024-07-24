using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static ArtifactManagerSO;

public abstract class ArtifactStat
{
    protected Artifact artifact;

    public ArtifactStatsInfo statInfo { get; protected set; }
    public float statsValue { get; protected set; }

    public ArtifactStat(Artifact Artifact)
    {
        statsValue = 0;
        artifact = Artifact;
    }

    public abstract void Upgrade();
}

