using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PartyMemberInfo : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI NameTxt;
    [SerializeField] private Image PartyIconImage;
    [SerializeField] private Healthbar_Characters Healthbar_Characters;
    private CharacterDataStat characterDataStat;

    public void SetCharacterDataStat(CharacterDataStat CharacterDataStat)
    {
        characterDataStat = CharacterDataStat;

        if (characterDataStat == null)
        {
            return;
        }

        NameTxt.text = characterDataStat.damageableEntitySO.GetName();
        PartyIconImage.sprite = GetCharacterIcon();
        Healthbar_Characters.SetCharacterDataStat(characterDataStat);
    }

    private Sprite GetCharacterIcon()
    {
        PlayableCharacterDataStat playableCharacterDataStat = characterDataStat as PlayableCharacterDataStat;

        if (playableCharacterDataStat != null)
        {
            return playableCharacterDataStat.playerCharactersSO.PartyCharacterIcon;
        }

        return characterDataStat.damageableEntitySO.GetIcon();
    }
}
