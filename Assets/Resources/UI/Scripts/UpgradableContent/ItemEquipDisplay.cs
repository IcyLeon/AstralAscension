using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquipDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI EquipTxt;
    [SerializeField] private Image PartyIconImage;

    public void UpdateVisual(UpgradableItems upgradableItems)
    {
        PlayerCharactersSO playerCharactersSO = upgradableItems.equipByCharacter as PlayerCharactersSO;

        gameObject.SetActive(playerCharactersSO != null);

        if (playerCharactersSO == null)
            return;

        EquipTxt.text = "Equipped: " + playerCharactersSO.GetName();
        PartyIconImage.sprite = playerCharactersSO.PartyCharacterIcon;
    }
}
