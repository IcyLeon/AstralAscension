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
    private ItemContentDisplay ItemContentDisplay;
    private UpgradableItems upgradableItem;

    private void Awake()
    {
        ItemContentDisplay = GetComponentInParent<ItemContentDisplay>();
        ItemContentDisplay.OnItemContentDisplayChanged += ItemContentDisplay_OnItemContentDisplayChanged;
    }

    private void ItemContentDisplay_OnItemContentDisplayChanged()
    {
        upgradableItem = ItemContentDisplay.iItem as UpgradableItems;
        UpdateVisual();
    }

    private void OnDestroy()
    {
        ItemContentDisplay.OnItemContentDisplayChanged -= ItemContentDisplay_OnItemContentDisplayChanged;
    }

    public void UpdateVisual()
    {
        PlayerCharactersSO playerCharactersSO = upgradableItem.equipByCharacter as PlayerCharactersSO;

        gameObject.SetActive(playerCharactersSO != null);

        if (playerCharactersSO == null)
            return;

        EquipTxt.text = "Equipped: " + playerCharactersSO.GetName();
        PartyIconImage.sprite = playerCharactersSO.PartyCharacterIcon;
    }
}
