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
        ItemCard.SetIItem(iEXPEntity.GetIEntity());
        UpdateUpgradableItemVisual();
    }

    private void UpdateUpgradableItemVisual()
    {
        UpgradableItems upgradableItems = iEXPEntity as UpgradableItems;

        EquipCharacterIconImage.gameObject.SetActive(upgradableItems != null && upgradableItems.equipByCharacter != null);

        if (!EquipCharacterIconImage.gameObject.activeSelf)
            return;

        EquipCharacterIconImage.sprite = upgradableItems.equipByCharacter.GetIcon();
    }
}
