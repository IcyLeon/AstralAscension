using System;
using UnityEngine;

[CreateAssetMenu(fileName = "PlayerCharactersSO", menuName = "ScriptableObjects/PlayerCharactersSO")]
public class PlayerCharactersSO : CharactersSO
{
    [Serializable]
    public class AscensionInformation
    {
        public AnimationCurve BaseHP;
        public AnimationCurve BaseATK;
        public AnimationCurve BaseDEF;
    }

    [Header("Player Character Infomation")]
    public Sprite partyCharacterIcon;
    public AscensionInformation[] ascensionInformation;
}
