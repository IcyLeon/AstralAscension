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
    }

    private void OnEnable()
    {
        ItemContentDisplay.OnItemContentDisplayChanged += ItemContentDisplay_OnItemContentDisplayChanged;
    }

    private void OnDisable()
    {
        ItemContentDisplay.OnItemContentDisplayChanged -= ItemContentDisplay_OnItemContentDisplayChanged;
    }

    private void ItemContentDisplay_OnItemContentDisplayChanged()
    {
        UnsubscribeEvents();
        upgradableItem = ItemContentDisplay.iData as UpgradableItems;
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
        if (!IsValid())
            return;

        PlayerCharactersSO playerCharactersSO = upgradableItem.equipByCharacter as PlayerCharactersSO;

        EquipTxt.text = playerCharactersSO.GetName();
        PartyIconImage.sprite = playerCharactersSO.PartyCharacterIcon;
    }

    private bool IsValid()
    {
        bool valid = upgradableItem != null && upgradableItem.equipByCharacter != null;
        MainPanel.SetActive(valid);
        return valid;
    }
}
