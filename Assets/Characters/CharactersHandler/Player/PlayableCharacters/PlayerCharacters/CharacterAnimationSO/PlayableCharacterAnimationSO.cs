using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayableCharacterAnimationSO", menuName = "ScriptableObjects/PlayableCharacterAnimation/PlayableCharacterAnimationSO")]
public class PlayableCharacterAnimationSO : ScriptableObject
{
    [Serializable]
    public class CommonPlayableCharacterHash
    {
        [Header("Blending Movement")]
        public string movementParameters;

        [Header("Animations")]
        public string dashParameter;
        public string weakStopParameter;
        public string strongStopParameter;
        public string fallParameter;
        public string jumpParameter;
        public string landParameter;
        public string plungeParameter;
    }

    [field: SerializeField] public CommonPlayableCharacterHash CommonPlayableCharacterHashParameters { get; private set; }
}
