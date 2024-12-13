using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemQualityCharacter : ItemQualityIEntity
{
    private PlayableCharacterDataStat playableCharacterDataStat
    {
        get
        {
            return iEntity as PlayableCharacterDataStat;
        }
    }

    protected override void UpdateVisual()
    {
        UpdateDisplayText("Lv." + playableCharacterDataStat.GetLevel());
    }
}
