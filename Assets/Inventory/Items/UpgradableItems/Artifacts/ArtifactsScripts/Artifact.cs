using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : UpgradableItems
{
    public class ArtifactStat
    {
        public float statsValue;

    }

    public ArtifactSO artifactSO
    {
        get
        {
            return iItem as ArtifactSO;
        }
    }

    public Artifact(Rarity Rarity, IItem iItem) : base(Rarity, iItem)
    {
    }
}
