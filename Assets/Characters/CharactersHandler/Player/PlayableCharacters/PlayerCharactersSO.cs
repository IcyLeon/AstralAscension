using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharactersSO", menuName = "ScriptableObjects/PlayerCharactersSO")]
public class PlayerCharactersSO : DamageableEntitySO
{
    [Serializable]
    public class AscensionInfo
    {
        public AnimationCurve BaseHP;
        public AnimationCurve BaseATK;
        public AnimationCurve BaseDEF;
    }

    [field: SerializeField] public float ElementalSkillRange { get; private set; } = 1f;

    [field: SerializeField] public Rarity Rarity { get; private set; } = Rarity.ONE_STAR;
    [field: SerializeField] public AscensionInfo[] AscensionInformation { get; private set; }
    [field: SerializeField] public PlayableCharacterAnimationSO PlayableCharacterAnimationSO { get; private set; }
    [field: SerializeField] public PlayableCharacterVoicelinesSO PlayableCharacterVoicelinesSO { get; private set; }

    [Header("Player Character Infomation")]
    public Sprite partyCharacterIcon;

    [Header("Skills")]
    public Sprite[] ElementalSkillIcons;
    public Sprite[] ElementalBurstIcons;
}
