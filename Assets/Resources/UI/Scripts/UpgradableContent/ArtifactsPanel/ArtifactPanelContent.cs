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
    [SerializeField] private ScrollRect ScrollRect;
    private TabGroup TabGroup;
    public ItemTypeTabGroup ItemTypeTabGroup { get; private set; }
    private Dictionary<ItemTypeSO, Transform> tabPanelDic;
    public event Action<ItemQualityButton> OnItemQualitySelect;
    private ItemManagerSO ItemAssetManagerSO;
    private Dictionary<IItem, ItemQualityButton> itemQualityDictionary;
    private Inventory inventory;

    private void Awake()
    {
        itemQualityDictionary = new();
        TabGroup = GetComponentInChildren<TabGroup>();
        TabGroup.OnTabOptionChanged += TabGroup_OnTabOptionChanged;
        ItemTypeTabGroup = TabGroup.GetComponent<ItemTypeTabGroup>();
        OnInventoryOld += InventoryManager_OnInventoryOld;
        OnInventoryNew += InventoryManager_OnInventoryNew;
    }
    private void Start()
    {
        ItemAssetManagerSO = AssetManager.instance.ItemAssetManagerSO;
        InitTabPanelDic();
        Init();
    }

    private void InitTabPanelDic()
    {
        if (tabPanelDic != null)
            return;

        tabPanelDic = new();

        ItemTypeTabOption[] itemTypeTabOptions = GetComponentsInChildren<ItemTypeTabOption>(true);

        foreach(var itemTypeTabOption in itemTypeTabOptions)
        {
            ItemTypeSO ItemTypeSO = itemTypeTabOption.ItemTypeSO;

            if (GetPanel(ItemTypeSO) != null)
                continue;

            tabPanelDic.Add(ItemTypeSO, itemTypeTabOption.GetPanel());
        }
    }

    private Transform GetPanel(ItemTypeSO ItemTypeSO)
    {
        if (tabPanelDic.TryGetValue(ItemTypeSO, out Transform transform))
            return transform;

        return null;
    }

    private void TabGroup_OnTabOptionChanged(TabOption TabOption)
    {
        ScrollRect.content = TabOption.Panel;
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

    private void Inventory_OnItemAdd(Item Item)
    {
        Transform Panel = GetPanel(Item.GetIItem().GetTypeSO());

        if (Panel == null)
            return;

        ItemQualityButton ItemQualityIEntity = ItemAssetManagerSO.CreateItemQualityItem(Item, Panel.transform);
        ItemQualityIEntity.OnItemQualitySelect += OnSelectedItemQuality;
        itemQualityDictionary.Add(Item, ItemQualityIEntity);
    }

    private void OnSelectedItemQuality(ItemQualityButton ItemQualityButton)
    {
        OnItemQualitySelect?.Invoke(ItemQualityButton);
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
        TabGroup.OnTabOptionChanged -= TabGroup_OnTabOptionChanged;
        OnInventoryOld -= InventoryManager_OnInventoryOld;
        OnInventoryNew -= InventoryManager_OnInventoryNew;

        if (inventory != null)
        {
            inventory.OnItemAdd -= Inventory_OnItemAdd;
            inventory.OnItemRemove -= Inventory_OnItemRemove;
        }
    }
}
