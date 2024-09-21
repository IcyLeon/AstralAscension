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

    private CharacterStorage characterStorage;

    [field: SerializeField] public CharactersSO charactersSO { get; private set; } // temp

    private Button EquipBtn;

    private void Awake()
    {
        CharacterManager.OnCharacterStorageOld += CharacterManager_OnCharacterStorageOld;
        CharacterManager.OnCharacterStorageNew += CharacterManager_OnCharacterStorageNew;

        ItemContentDisplay.OnItemContentDisplayChanged += ItemContentDisplay_OnItemContentDisplayChanged;
        UpdateVisuals();
        EquipBtn = GetComponent<Button>();

        if (EquipBtn == null)
            return;

        EquipBtn.onClick.AddListener(OnEquip);
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
        SetUpgradableItems(e.iItem as UpgradableItems);
    }

    private void SetUpgradableItems(UpgradableItems UpgradableItems)
    {
        UnsubscribeEvents();
        upgradableItems = UpgradableItems;
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

        upgradableItems.OnItemChanged -= UpgradableItems_OnItemChanged;
    }

    private void UpgradableItems_OnItemChanged(object sender, System.EventArgs e)
    {
        UpdateVisuals();
    }

    private void SubscribeEvents()
    {
        if (upgradableItems == null)
            return;

        upgradableItems.OnItemChanged += UpgradableItems_OnItemChanged;
        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        gameObject.SetActive(upgradableItems != null);

        if (upgradableItems == null)
        {
            ResetEquipStatus();
            return;
        }


        if (upgradableItems.equipByCharacter != null)
        {
            if (upgradableItems.equipByCharacter != charactersSO &&
                characterStorage.playableCharacterStatList.TryGetValue(charactersSO, out PlayableCharacterDataStat playableCharacterDataStat))
            {
                if (playableCharacterDataStat.GetItem(upgradableItems.GetTypeSO()) != null)
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
            upgradableItems.SetEquip(charactersSO);
            return;
        }

        upgradableItems.SetEquip(null);
    }
}
