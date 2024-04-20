using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayableCharacterVoicelinesSO", menuName = "ScriptableObjects/PlayableCharacterVoicelines/PlayableCharacterVoicelinesSO")]
public class PlayableCharacterVoicelinesSO : ScriptableObject
{
    [SerializeField] private AudioClip[] JumpVOClips;
    [SerializeField] private AudioClip[] FallenVOClips;
    [SerializeField] private AudioClip[] DashVOClips;
    [SerializeField] private AudioClip[] BasicAttackVOClips;
    [SerializeField] private AudioClip[] ElementalSkillVOClips;
    [SerializeField] private AudioClip[] ElementalSkill_RecastVOClips;
    [SerializeField] private AudioClip[] ElementalBurstVOClips;
    [SerializeField] private AudioClip[] LightInjuredVOClips;

    private AudioClip GetRandomAudioClip(AudioClip[] clip)
    {
        CharacterManager characterManager = CharacterManager.instance;
        if (characterManager == null)
            return null;

        if (!CharacterManager.isInProbabilityRange(characterManager.GetProbabilityPlayVO()))
            return null;

        int randomIdx = Random.Range(0, clip.Length);
        if (clip.Length == 0)
            return null;

        return clip[randomIdx];
    }

    public AudioClip GetRandomJumpVOClip()
    {
        return GetRandomAudioClip(JumpVOClips);
    }
    public AudioClip GetRandomFallenVOClip()
    {
        return GetRandomAudioClip(FallenVOClips);
    }
    public AudioClip GetRandomDashVOClip()
    {
        return GetRandomAudioClip(DashVOClips);
    }
    public AudioClip GetRandomBasicAttackVOClip()
    {
        return GetRandomAudioClip(BasicAttackVOClips);
    }
    public AudioClip GetRandomElementalSkillVOClip()
    {
        return GetRandomAudioClip(ElementalSkillVOClips);
    }

    public AudioClip GetRandomElementalSkill_RecastVOClip()
    {
        return GetRandomAudioClip(ElementalSkill_RecastVOClips);
    }
}
