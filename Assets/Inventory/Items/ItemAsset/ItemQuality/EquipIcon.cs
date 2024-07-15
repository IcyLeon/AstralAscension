using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EquipIcon : MonoBehaviour
{
    [SerializeField] private Image IconImage;

    public void UpdateVisual(Item item)
    {
        UpgradableItems upgradableItems = item as UpgradableItems;

        gameObject.SetActive(upgradableItems != null);

        if (upgradableItems == null)
            return;

        PlayerCharactersSO playerCharactersSO = upgradableItems.equipByCharacter as PlayerCharactersSO;
        gameObject.SetActive(playerCharactersSO != null);

        if (playerCharactersSO == null)
            return;

        IconImage.sprite = playerCharactersSO.partyCharacterIcon;
    }
}
