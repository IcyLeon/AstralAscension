using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "AscensionSO", menuName = "ScriptableObjects/PlayerCharactersManager/AscensionSO")]
public class AscensionSO : ScriptableObject
{
    [Serializable]
    public class AscensionInfo
    {
        [field: SerializeField] public AnimationCurve BaseHP { get; private set; }
        [field: SerializeField] public AnimationCurve BaseATK { get; private set; }
        [field: SerializeField] public AnimationCurve BaseDEF { get; private set; }
    }

    [Serializable]
    public class ConstellationInfo
    {
        [field: SerializeField] public string Description { get; private set; }
    }

    [field: SerializeField] public AscensionInfo[] AscensionInformation { get; private set; }
    [field: SerializeField] public ConstellationInfo[] ConstellationInformation { get; private set; }

}
