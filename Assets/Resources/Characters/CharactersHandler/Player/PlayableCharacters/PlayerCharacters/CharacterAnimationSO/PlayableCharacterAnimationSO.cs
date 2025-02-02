using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayableCharacterAnimationSO", menuName = "ScriptableObjects/PlayableCharacterAnimation/PlayableCharacterAnimationSO")]
public class PlayableCharacterAnimationSO : ScriptableObject
{
    [Serializable]
    public class ElementalStateHash
    {
        [field: SerializeField] public string elementalSkillParameter { get; private set; }
        [field: SerializeField] public string elementalBurstParameter { get; private set; }
    }
   
    [Serializable]
    public class CommonPlayableCharacterHash
    {
        [field: SerializeField, Header("Blending Movement")] public string movementParameters { get; private set; }

        [field: SerializeField, Header("Entering State")] public string groundParameter { get; private set; }
        [field: SerializeField] public string elementalStateParameter { get; private set; }
        [field: SerializeField] public string dashParameter { get; private set; }
        [field: SerializeField] public string fallParameter { get; private set; }
        [field: SerializeField] public string jumpParameter { get; private set; }
        [field: SerializeField] public string plungeParameter { get; private set; }
        [field: SerializeField] public string attackParameter { get; private set; }
        [field: SerializeField] public string stopParameter { get; private set; }
        [field: SerializeField] public string landParameter { get; private set; }
        [field: SerializeField] public string movingParameter { get; private set; }
        [field: SerializeField] public string idleParameter { get; private set; }

        [field: SerializeField, Header("Triggers")] public ElementalStateHash elementalStateHash { get; private set; }
        [field: SerializeField] public string deadParameter { get; private set; }
        [field: SerializeField] public string weakLandParameter { get; private set; }
        [field: SerializeField] public string hardLandParameter { get; private set; }
        [field: SerializeField] public string plungeLandingParameter { get; private set; }
        [field: SerializeField] public string weakStopParameter { get; private set; }
        [field: SerializeField] public string dashStopParameter { get; private set; }
        [field: SerializeField] public string strongStopParameter { get; private set; }
    }

    [field: SerializeField] public CommonPlayableCharacterHash CommonPlayableCharacterHashParameters { get; private set; }
}
