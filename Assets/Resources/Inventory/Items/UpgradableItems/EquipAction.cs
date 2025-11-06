using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipAction : MonoBehaviour
{
    [SerializeField] private ItemContentDisplay ItemContentDisplay;
    [SerializeField] private TextMeshProUGUI EquipTxt;
    private UpgradableItems upgradableItem;
    private CharacterScreenPanel characterScreenPanel;
    private CharacterEquipmentManager characterEquipmentManager;
    private Button EquipBtn;
    private InventoryManager inventoryManager;

    private void Awake()
    {
        characterScreenPanel = GetComponentInParent<CharacterScreenPanel>();
        characterScreenPanel.OnCharacterIconSelected += OwnerCharacterUIManager_OnIconSelected;
        EquipBtn = GetComponent<Button>();
        EquipBtn.onClick.AddListener(OnEquip);
        UpdateIconSelected();
    }

    private void Start()
    {
        inventoryManager = InventoryManager.instance;
    }

    private void OnEnable()
    {
        ItemContentDisplay.OnItemContentDisplayChanged += ItemContentDisplay_OnItemContentDisplayChanged;
    }

    private void OnDisable()
    {
        ItemContentDisplay.OnItemContentDisplayChanged -= ItemContentDisplay_OnItemContentDisplayChanged;
    }

    private void OwnerCharacterUIManager_OnIconSelected()
    {
        UpdateIconSelected();
    }

    private void UpdateIconSelected()
    {
        characterEquipmentManager = characterScreenPanel.characterEquipmentManager;
    }

    private void ItemContentDisplay_OnItemContentDisplayChanged()
    {
        UnsubscribeEvents();
        upgradableItem = ItemContentDisplay.iData as UpgradableItems;
        SubscribeEvents();
    }


    private void OnDestroy()
    {
        UnsubscribeEvents();
        characterScreenPanel.OnCharacterIconSelected -= OwnerCharacterUIManager_OnIconSelected;
        EquipBtn.onClick.RemoveAllListeners();
    }


    private void UnsubscribeEvents()
    {
        if (upgradableItem == null)
            return;

        upgradableItem.OnIEntityChanged -= UpgradableItems_OnIEntityChanged;
    }

    private void SubscribeEvents()
    {
        if (upgradableItem == null)
            return;

        upgradableItem.OnIEntityChanged += UpgradableItems_OnIEntityChanged;
        UpdateVisuals();
    }

    private void UpgradableItems_OnIEntityChanged(IEntity e)
    {
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (IsValid())
        {
            UpdateEquipTextVisual();
            return;
        }

        ResetEquipStatus();
    }

    private bool IsValid()
    {
        bool valid = upgradableItem != null;
        gameObject.SetActive(valid);
        return valid && upgradableItem.equipByCharacter;
    }

    private void UpdateEquipTextVisual()
    {
        if (upgradableItem.equipByCharacter != characterEquipmentManager.charactersSO)
        {
            EquipTxt.text = "Switch";
            return;
        }

        EquipTxt.text = "Remove";
    }

    private void ResetEquipStatus()
    {
        EquipTxt.text = "Equip";
    }

    private void OnEquip()
    {
        if (upgradableItem == null)
            return;

        if (upgradableItem.equipByCharacter == null || upgradableItem.equipByCharacter != characterEquipmentManager.charactersSO)
        {
            inventoryManager.EquipItem(characterEquipmentManager.charactersSO, upgradableItem);
            return;
        }

        inventoryManager.UnequipItem(characterEquipmentManager.charactersSO, upgradableItem);
    }
}
