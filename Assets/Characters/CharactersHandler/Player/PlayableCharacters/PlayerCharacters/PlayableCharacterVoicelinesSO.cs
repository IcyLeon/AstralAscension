using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayableCharacterVoicelinesSO", menuName = "ScriptableObjects/PlayableCharacterVoicelines/PlayableCharacterVoicelinesSO")]
public class PlayableCharacterVoicelinesSO : ScriptableObject
{
    [field: SerializeField] public AudioClip[] JumpVOClips { get; private set; }
    [field: SerializeField] public AudioClip[] FallenVOClips { get; private set; }
    [field: SerializeField] public AudioClip[] DashVOClips { get; private set; }
    [field: SerializeField] public AudioClip[] BasicAttackVOClips { get; private set; }
    [field: SerializeField] public AudioClip[] ElementalSkillVOClips { get; private set; }
    [field: SerializeField] public AudioClip[] ElementalBurstVOClips { get; private set; }
    [field: SerializeField] public AudioClip[] LightInjuredVOClips { get; private set; }
}
