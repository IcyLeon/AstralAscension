using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class DetailsPanel : EnhancementPanel
{
    [SerializeField] private ItemCard ItemCard;
    [SerializeField] private Image EquipCharacterIconImage;

    protected override void UpdateVisual()
    {
        base.UpdateVisual();
        ItemCard.SetIItem(upgradableItem);
        UpdateUpgradableItemVisual();
    }

    private void UpdateUpgradableItemVisual()
    {
        EquipCharacterIconImage.gameObject.SetActive(upgradableItem != null && upgradableItem.equipByCharacter != null);

        if (!EquipCharacterIconImage.gameObject.activeSelf)
            return;

        EquipCharacterIconImage.sprite = upgradableItem.equipByCharacter.GetIcon();
    }
}
