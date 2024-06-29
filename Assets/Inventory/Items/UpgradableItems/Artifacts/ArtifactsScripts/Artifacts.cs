using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Artifacts : UpgradableItems
{
    public float statsValue { get; private set; }
    public ArtifactsSO artifactsSO
    {
        get
        {
            return itemSO as ArtifactsSO;
        }
    }

    public Artifacts(Rarity Rarity, ItemSO ItemSO) : base(Rarity, ItemSO)
    {
    }
}
