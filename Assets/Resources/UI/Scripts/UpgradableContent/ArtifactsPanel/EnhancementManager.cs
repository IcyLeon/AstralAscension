using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static InventoryManager;

public class EnhancementManager : MonoBehaviour
{
    [SerializeField] private SlotManager slotManager;
    [SerializeField] private TMP_Dropdown dropdown;
    [SerializeField] private Button AutoAddBtn;
    [SerializeField] private Button EnhanceBtn;
    private Rarity raritySelection;
    private Inventory inventory;
    private EnhancePanel enhancePanel;

    private void Awake()
    {
        OnInventoryNew += InventoryManager_OnInventoryNew;
        OnInventoryOld += InventoryManager_OnInventoryOld;

        dropdown.onValueChanged.AddListener(delegate
        {
            OnDropdown(dropdown);
        });

        enhancePanel = GetComponentInParent<EnhancePanel>();
        enhancePanel.OnUpgradableItemChanged += EnhancePanel_OnUpgradableItemChanged;

        AutoAddBtn.onClick.AddListener(OnAutoAdd);
        EnhanceBtn.onClick.AddListener(OnEnhance);
    }

    private void Start()
    {
        Init();
    }

    private void OnDropdown(TMP_Dropdown dropdown)
    {
        raritySelection = (Rarity)dropdown.value;
    }

    private void EnhancePanel_OnUpgradableItemChanged(object sender, System.EventArgs e)
    {
        OnItemChanged();
    }

    private void Init()
    {
        if (instance == null)
        {
            Debug.LogError("Inventory Manager  not found!");
            return;
        }

        InventoryManager_OnInventoryNew(instance.inventory);
    }

    private void ResetSlots()
    {
        slotManager.ResetAllSlots();
    }

    private void InventoryManager_OnInventoryOld(Inventory Inventory)
    {
    }

    private void InventoryManager_OnInventoryNew(Inventory Inventory)
    {
        inventory = Inventory;
        if (inventory != null)
        {
            OnItemChanged();
        }
    }

    private void OnItemChanged()
    {
        ResetSlots();
        slotManager.SetinterfaceItemType(enhancePanel.iItem);
    }

    private void BubbleSortRarities(ref List<UpgradableItems> list)
    {
        for(int i = 0; i < list.Count - 1; i++)
        {
            for (int j = 0; j < list.Count - i - 1; j++)
            {
                if (list[j].GetItemRarity() > list[j + 1].GetItemRarity())
                {
                    Swap(ref list, j, j + 1);
                }
            }
        }
    }

    private void Swap(ref List<UpgradableItems> list, int first, int second)
    {
        var temp = list[first];
        list[first] = list[second];
        list[second] = temp;
    }

    private List<UpgradableItems> GetRelatedItemList(List<Item> list)
    {
        List<UpgradableItems> UpgradableItems = new();

        foreach(var item in list)
        {
            UpgradableItems upgradableItem = item as UpgradableItems;
            if (upgradableItem != null && 
                upgradableItem.GetItemRarity() <= raritySelection)
            {
                UpgradableItems.Add(upgradableItem);
            }
        }

        return UpgradableItems;
    }

    private void OnAutoAdd()
    {
        List<UpgradableItems> allrelatedItems = GetRelatedItemList(slotManager.GetItemList());

        for (int i = 0; i < allrelatedItems.Count; i++)
        {
            int randomValue = Random.Range(0, allrelatedItems.Count);
            Swap(ref allrelatedItems, i, randomValue);
        }

        BubbleSortRarities(ref allrelatedItems);

        for (int i = 0; i < allrelatedItems.Count; i++)
        {
            Slot emptySlot = slotManager.GetAvailableSlot();

            if (emptySlot == null)
                return;

            UpgradableItems item = allrelatedItems[i];

            if (item.locked || item.equipByCharacter != null || slotManager.Contains(item))
                continue;

            emptySlot.SetItemQualityButton(item);

        }
    }

    private void OnEnhance()
    {
        ResetSlots();

    }

    // Update is called once per frame
    private void OnDestroy()
    {
        OnInventoryNew -= InventoryManager_OnInventoryNew;
        OnInventoryOld -= InventoryManager_OnInventoryOld;

        if (enhancePanel != null)
            enhancePanel.OnUpgradableItemChanged -= EnhancePanel_OnUpgradableItemChanged;
    }
}
