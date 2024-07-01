using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifact : UpgradableItems
{
    public float statsValue { get; private set; }
    public ArtifactSO artifactSO
    {
        get
        {
            return itemSO as ArtifactSO;
        }
    }

    public Artifact(Rarity Rarity, ItemSO ItemSO) : base(Rarity, ItemSO)
    {
    }
}
