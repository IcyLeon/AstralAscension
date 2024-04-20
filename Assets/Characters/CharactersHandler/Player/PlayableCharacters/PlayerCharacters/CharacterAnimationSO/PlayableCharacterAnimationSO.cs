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

        [Header("Entering State")]
        public string groundParameter;
        public string elementalStateParameter;

        [Header("Animations")]
        [Header("Bool")]
        public string dashParameter;
        public string fallParameter;
        public string jumpParameter;
        public string plungeParameter;
        public string plungeLandingParameter;
        public string attackingParameter;

        [Header("Triggers")]
        public string deadParameter;        
        public string weakLandParameter;
        public string hardLandParameter;        
        public string weakStopParameter;
        public string strongStopParameter;
    }

    [field: SerializeField] public CommonPlayableCharacterHash CommonPlayableCharacterHashParameters { get; private set; }
}
