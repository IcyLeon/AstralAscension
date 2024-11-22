using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharactersSO", menuName = "ScriptableObjects/PlayerCharactersManager/PlayerCharactersSO")]
public class PlayerCharactersSO : DamageableEntitySO
{
    [Serializable]
    public class AscensionInfo
    {
        public AnimationCurve BaseHP;
        public AnimationCurve BaseATK;
        public AnimationCurve BaseDEF;
    }

    [field: SerializeField] public AscensionInfo[] AscensionInformation { get; private set; }
    [field: SerializeField] public PlayableCharacterAnimationSO PlayableCharacterAnimationSO { get; private set; }
    [field: SerializeField] public PlayableCharacterVoicelinesSO PlayableCharacterVoicelinesSO { get; private set; }

    [field: SerializeField, Header("Player Character Infomation")] public Sprite PartyCharacterIcon;

    [field: SerializeField, Header("Skills")] public PlayableCharacterSkillSO ElementalSkillInfo { get; private set; }
    [field: SerializeField] public PlayableCharacterSkillSO ElementalBurstInfo { get; private set; }

    [field: SerializeField] public int BurstEnergyCost { get; private set; }

    public override CharacterDataStat CreateCharacterDataStat()
    {
        return new PlayableCharacterDataStat(this);
    }

}
