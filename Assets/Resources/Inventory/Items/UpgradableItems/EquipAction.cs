using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipAction : MonoBehaviour
{
    [SerializeField] private ItemContentDisplay ItemContentDisplay;
    [SerializeField] private TextMeshProUGUI EquipTxt;
    private UpgradableItems upgradableItems;
    private CharacterScreenPanel characterScreenPanel;
    private CharactersSO charactersSO;
    private Button EquipBtn;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        characterScreenPanel = GetComponentInParent<CharacterScreenPanel>();
        characterScreenPanel.OnIconSelected += OwnerCharacterUIManager_OnIconSelected;
        ItemContentDisplay.OnItemContentDisplayChanged += ItemContentDisplay_OnItemContentDisplayChanged;

        EquipBtn = GetComponent<Button>();
        EquipBtn.onClick.AddListener(OnEquip);
        UpdateIconSelected();
    }

    private void Start()
    {
        inventoryManager = InventoryManager.instance;
    }

    private void OwnerCharacterUIManager_OnIconSelected()
    {
        UpdateIconSelected();
    }

    private void UpdateIconSelected()
    {
        CharacterDataStat characterDataStat = characterScreenPanel.currentCharacterSelected;

        if (characterDataStat == null)
            return;

        charactersSO = characterDataStat.damageableEntitySO;
    }

    private void ItemContentDisplay_OnItemContentDisplayChanged()
    {
        UnsubscribeEvents();
        upgradableItems = ItemContentDisplay.iItem as UpgradableItems;
        SubscribeEvents();
    }


    private void OnDestroy()
    {
        UnsubscribeEvents();
        characterScreenPanel.OnIconSelected -= OwnerCharacterUIManager_OnIconSelected;
        ItemContentDisplay.OnItemContentDisplayChanged -= ItemContentDisplay_OnItemContentDisplayChanged;

        EquipBtn.onClick.RemoveAllListeners();
    }


    private void UnsubscribeEvents()
    {
        if (upgradableItems == null)
            return;

        upgradableItems.OnIEntityChanged -= UpgradableItems_OnIEntityChanged;
    }

    private void SubscribeEvents()
    {
        if (upgradableItems == null)
            return;

        upgradableItems.OnIEntityChanged += UpgradableItems_OnIEntityChanged;
        UpdateVisuals();
    }

    private void UpgradableItems_OnIEntityChanged(IEntity e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        gameObject.SetActive(upgradableItems != null);

        if (upgradableItems != null && upgradableItems.equipByCharacter != null)
        {
            if (upgradableItems.equipByCharacter != charactersSO)
            {
                EquipTxt.text = "Switch";
                return;
            }

            EquipTxt.text = "Remove";
            return;
        }

        ResetEquipStatus();
    }

    private void ResetEquipStatus()
    {
        EquipTxt.text = "Equip";
    }

    private void OnEquip()
    {
        if (upgradableItems == null)
            return;

        if (upgradableItems.equipByCharacter == null || upgradableItems.equipByCharacter != charactersSO)
        {
            inventoryManager.EquipItem(charactersSO, upgradableItems);
            return;
        }

        inventoryManager.UnequipItem(charactersSO, upgradableItems);
    }
}
