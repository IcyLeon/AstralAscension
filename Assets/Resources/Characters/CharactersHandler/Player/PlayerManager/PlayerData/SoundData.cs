using System;
using UnityEngine;

[Serializable]
public class SoundData
{
    [field: SerializeField] public AudioClip[] DashClips { get; private set; }
    [field: SerializeField] public AudioClip SwitchClip { get; private set; }
}
