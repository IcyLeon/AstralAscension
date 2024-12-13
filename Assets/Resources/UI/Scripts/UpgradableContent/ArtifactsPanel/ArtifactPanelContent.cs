using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using static InventoryManager;

[DisallowMultipleComponent]
public class ArtifactPanelContent : MonoBehaviour
{
    [Serializable]
    public class ArtifactPanel
    {
        [field: SerializeField] public TabOption tabOption { get; private set; }
        [field: SerializeField] public ItemTypeSO ItemTypeSO { get; private set; }
    }

    [SerializeField] private ScrollRect ScrollRect;
    [SerializeField] private ItemContentDisplay ItemContentDisplay;
    [SerializeField] private TabGroup TabGroup;
    [SerializeField] private ArtifactPanel[] ArtifactPanelList;
    private ItemManagerSO ItemAssetManagerSO;
    private Dictionary<IItem, ItemQualityButton> itemQualityDictionary;
    private Inventory inventory;

    private void Awake()
    {
        itemQualityDictionary = new();
        TabGroup.OnTabGroupChanged += TabGroup_OnTabGroupChanged;
        OnInventoryOld += InventoryManager_OnInventoryOld;
        OnInventoryNew += InventoryManager_OnInventoryNew;
    }

    private void Init()
    {
        if (instance == null)
        {
            Debug.LogError("Inventory Manager not found!");
            return;
        }

        InventoryManager_OnInventoryNew(instance.inventory);
    }

    private void Start()
    {
        ItemAssetManagerSO = AssetManager.instance.ItemAssetManagerSO;
        Init();
    }

    private void TabGroup_OnTabGroupChanged(TabOption TabOption)
    {
        ScrollRect.content = TabOption.Panel;
    }

    private void InventoryManager_OnInventoryOld(Inventory Inventory)
    {
        Inventory.OnItemAdd -= Inventory_OnItemAdd;
        Inventory.OnItemRemove -= Inventory_OnItemRemove;
    }

    private void InventoryManager_OnInventoryNew(Inventory Inventory)
    {
        if (inventory == Inventory)
            return;

        inventory = Inventory;
        if (inventory != null)
        {
            inventory.OnItemAdd += Inventory_OnItemAdd;
            inventory.OnItemRemove += Inventory_OnItemRemove;
            UpdateVisual();
        }
    }

    private void Inventory_OnItemRemove(Item Item)
    {
        if (itemQualityDictionary.TryGetValue(Item, out ItemQualityButton itemQualityButton))
        {
            itemQualityButton.Destroy();
            itemQualityDictionary.Remove(Item);
        }
    }

    private GameObject GetPanel(ItemTypeSO itemTypeSO)
    {
        foreach(var ArtifactPanel in ArtifactPanelList)
        {
            if (ArtifactPanel.ItemTypeSO == itemTypeSO)
                return ArtifactPanel.tabOption.Panel.gameObject;
        }
        return null;
    }

    private void Inventory_OnItemAdd(Item Item)
    {
        if (Item == null)
            return;

        GameObject Panel = GetPanel(Item.GetIItem().GetTypeSO());

        if (Panel == null)
            return;

        ItemQualityButton ItemQualityIEntity = ItemAssetManagerSO.CreateItemQualityItem(Item, Panel.transform);
        ItemQualityIEntity.OnItemQualitySelect += OnSelectedItemQuality;
        itemQualityDictionary.Add(Item, ItemQualityIEntity);
    }

    private void OnSelectedItemQuality(ItemQualityButton ItemQualityButton)
    {
        ItemContentDisplay.SetIItem(ItemQualityButton.ItemQuality.iItem);
    }

    private void UpdateVisual()
    {
        if (itemQualityDictionary == null)
            return;

        for (int i = itemQualityDictionary.Count - 1; i >= 0; i--)
        {
            Inventory_OnItemRemove(itemQualityDictionary.ElementAt(i).Key as Item);
        }

        itemQualityDictionary.Clear();

        foreach (var item in inventory.itemList)
        {
            Inventory_OnItemAdd(item.Key);
        }
    }

    private void OnDestroy()
    {
        TabGroup.OnTabGroupChanged -= TabGroup_OnTabGroupChanged;
        OnInventoryOld -= InventoryManager_OnInventoryOld;
        OnInventoryNew -= InventoryManager_OnInventoryNew;

        if (inventory != null)
        {
            inventory.OnItemAdd -= Inventory_OnItemAdd;
            inventory.OnItemRemove -= Inventory_OnItemRemove;
        }
    }
}
