using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[Serializable]
public class AscensionInfoStat
{
    [field: SerializeField] public AnimationCurve BaseHP { get; private set; }
    [field: SerializeField] public AnimationCurve BaseATK { get; private set; }
    [field: SerializeField] public AnimationCurve BaseDEF { get; private set; }

    public Ascension CreateAscension()
    {
        return new Ascension(this);
    }
}

[CreateAssetMenu(fileName = "AscensionSO", menuName = "ScriptableObjects/PlayerCharactersManager/AscensionSO")]
public class AscensionSO : ScriptableObject
{
    [field: SerializeField] public AscensionInfoStat[] AscensionInfoStat { get; private set; }
}
