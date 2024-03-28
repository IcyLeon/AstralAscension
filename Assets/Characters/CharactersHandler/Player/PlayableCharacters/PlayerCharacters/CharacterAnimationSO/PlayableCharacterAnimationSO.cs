using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacterAnimationSO : ScriptableObject
{
    [Serializable]
    public class CommonPlayableCharacterHash
    {
        public string dashHashParameter;
        public string weakStopParameter;
        public string strongStopParameter;
    }

    [field: SerializeField] public CommonPlayableCharacterHash CommonPlayableCharacterHashParameters { get; private set; }
}
