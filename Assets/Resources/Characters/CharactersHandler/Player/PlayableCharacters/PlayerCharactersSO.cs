using System;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharactersSO", menuName = "ScriptableObjects/PlayerCharactersManager/PlayerCharactersSO")]
public class PlayerCharactersSO : DamageableEntitySO
{
    [field: SerializeField] public AscensionSO AscensionSO { get; private set; }
    [field: SerializeField] public PlayableCharacterAnimationSO PlayableCharacterAnimationSO { get; private set; }
    [field: SerializeField] public PlayableCharacterVoicelinesSO PlayableCharacterVoicelinesSO { get; private set; }

    [field: SerializeField, Header("Player Character Infomation")] public Sprite PartyCharacterIcon;

    [field: SerializeField, Header("Skills")] public ElementalSkillSO ElementalSkillInfo { get; private set; }
    [field: SerializeField] public ElementalBurstSO ElementalBurstInfo { get; private set; }
    public override CharacterDataStat CreateCharacterDataStat()
    {
        return new PlayableCharacterDataStat(this);
    }

    public PlayerCharacterProfileSO GetPlayerCharacterProfileSO()
    {
        return ProfileSO as PlayerCharacterProfileSO;
    }
}
