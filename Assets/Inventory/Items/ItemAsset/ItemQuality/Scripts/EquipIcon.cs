using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipIcon : MonoBehaviour
{
    [SerializeField] private Image IconImage;
    private UpgradableItems upgradableItem;

    public void SetIItem(IItem IItem)
    {
        UnsubscribeEvents();
        upgradableItem = IItem as UpgradableItems;
        SubscribeEvents();
    }
    private void SubscribeEvents()
    {
        if (upgradableItem == null)
            return;

        upgradableItem.OnIEntityChanged += UpgradableItem_OnIEntityChanged;
        UpdateVisual();
    }

    private void UpgradableItem_OnIEntityChanged(object sender, IEntityEvents e)
    {
        if (this == null)
            return;

        UpdateVisual();
    }

    private void UnsubscribeEvents()
    {
        if (upgradableItem == null)
            return;

        upgradableItem.OnIEntityChanged -= UpgradableItem_OnIEntityChanged;
    }

    private void UpdateVisual()
    {
        gameObject.SetActive(upgradableItem != null);

        if (upgradableItem == null)
            return;

        PlayerCharactersSO playerCharactersSO = upgradableItem.equipByCharacter as PlayerCharactersSO;
        gameObject.SetActive(playerCharactersSO != null);

        if (playerCharactersSO == null)
            return;

        IconImage.sprite = playerCharactersSO.GetIcon();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
