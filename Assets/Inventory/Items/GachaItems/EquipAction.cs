using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EquipAction : MonoBehaviour
{
    [SerializeField] private ItemContentDisplay ItemContentDisplay;
    [SerializeField] private TextMeshProUGUI EquipTxt;

    private InventoryManager inventoryManager;

    private UpgradableItems upgradableItems;

    private CharacterStorage characterStorage;

    [field: SerializeField] public CharactersSO charactersSO { get; private set; } // temp

    private Button EquipBtn;

    private void Awake()
    {
        CharacterManager.OnCharacterStorageOld += CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew += CharacterManager_OnCharacterStorageNew;

        ItemContentDisplay.OnItemContentDisplayChanged += ItemContentDisplay_OnItemContentDisplayChanged;

        EquipBtn = GetComponent<Button>();

        if (EquipBtn == null)
            return;

        EquipBtn.onClick.AddListener(OnEquip);
    }

    private void Start()
    {
        inventoryManager = InventoryManager.instance;
        SetIItem(ItemContentDisplay.iItem);
    }

    public void SetCharacterSO(CharactersSO charactersSO)
    {
        this.charactersSO = charactersSO;
    }

    private void CharacterManager_OnCharacterStorageOld(CharacterStorage CharacterStorage)
    {
    }
    private void CharacterManager_OnCharacterStorageNew(CharacterStorage CharacterStorage)
    {
        characterStorage = CharacterStorage;
    }

    private void ItemContentDisplay_OnItemContentDisplayChanged(object sender, ItemContentDisplay.ItemContentEvent e)
    {
        SetIItem(e.iItem);
    }

    private void SetIItem(IItem IItem)
    {
        UnsubscribeEvents();
        upgradableItems = IItem as UpgradableItems;
        SubscribeEvents();
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
        CharacterManager.OnCharacterStorageOld -= CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew -= CharacterManager_OnCharacterStorageNew;
        ItemContentDisplay.OnItemContentDisplayChanged -= ItemContentDisplay_OnItemContentDisplayChanged;
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
            if (upgradableItems.equipByCharacter != charactersSO &&
                characterStorage.playableCharacterStatList.TryGetValue(charactersSO, out PlayableCharacterDataStat playableCharacterDataStat))
            {
                if (playableCharacterDataStat.GetItem(upgradableItems.GetIItem().GetTypeSO()) != null)
                {
                    EquipTxt.text = "Switch";
                    return;
                }
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
