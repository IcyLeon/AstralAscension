using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipIcon : MonoBehaviour
{
    [SerializeField] private Image IconImage;
    private UpgradableItems upgradableItem;

    public void SetIItem(IData IData)
    {
        UnsubscribeEvents();
        upgradableItem = IData as UpgradableItems;
        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        if (upgradableItem == null)
            return;

        upgradableItem.OnIEntityChanged += UpgradableItem_OnIEntityChanged;
        UpdateVisual();
    }

    private void UpgradableItem_OnIEntityChanged(IEntity e)
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
        if (!IsValid())
            return;

        PlayerCharactersSO playerCharactersSO = upgradableItem.equipByCharacter as PlayerCharactersSO;
        IconImage.sprite = playerCharactersSO.PartyCharacterIcon;
    }

    private bool IsValid()
    {
        bool valid = upgradableItem != null && upgradableItem.equipByCharacter != null;
        gameObject.SetActive(valid);
        return valid;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
