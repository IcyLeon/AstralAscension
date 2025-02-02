using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemEquipDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI EquipTxt;
    [SerializeField] private GameObject MainPanel;
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
        UnsubscribeEvents();
        upgradableItem = ItemContentDisplay.iItem as UpgradableItems;
        SubscribeEvents();
        UpdateVisual();
    }

    private void UnsubscribeEvents()
    {
        if (upgradableItem == null)
            return;

        upgradableItem.OnIEntityChanged -= UpgradableItem_OnIEntityChanged;
    }

    private void UpgradableItem_OnIEntityChanged(IEntity IEntity)
    {
        if (this == null)
            return;

        UpdateVisual();
    }

    private void SubscribeEvents()
    {
        if (upgradableItem == null)
            return;

        upgradableItem.OnIEntityChanged += UpgradableItem_OnIEntityChanged;
        UpdateVisual();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
        ItemContentDisplay.OnItemContentDisplayChanged -= ItemContentDisplay_OnItemContentDisplayChanged;
    }

    private void UpdateVisual()
    {
        MainPanel.SetActive(upgradableItem != null && upgradableItem.equipByCharacter);

        if (upgradableItem == null)
            return;

        PlayerCharactersSO playerCharactersSO = upgradableItem.equipByCharacter as PlayerCharactersSO;

        if (playerCharactersSO == null)
            return;

        EquipTxt.text = "Equipped: " + playerCharactersSO.GetName();
        PartyIconImage.sprite = playerCharactersSO.PartyCharacterIcon;
    }
}
