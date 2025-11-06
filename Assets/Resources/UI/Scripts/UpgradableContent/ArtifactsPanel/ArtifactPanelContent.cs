using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class ArtifactPanelContent : MonoBehaviour
{
    [SerializeField] private ScrollRect ScrollRect;
    private TabGroup TabGroup;
    public ItemTypeTabGroup itemTypeTabGroup { get; private set; }
    private Dictionary<ItemTypeSO, Transform> tabPanelDic;
    public event Action<ItemQuality> OnItemQualitySelect;
    private ItemManagerSO ItemAssetManagerSO;
    private Dictionary<IData, ItemQuality> itemQualityDictionary;
    private Inventory inventory;

    private void Awake()
    {
        itemQualityDictionary = new();
        TabGroup = GetComponentInChildren<TabGroup>();
        TabGroup.OnTabOptionChanged += TabGroup_OnTabOptionChanged;
        itemTypeTabGroup = TabGroup.GetComponent<ItemTypeTabGroup>();
        InventoryManager.OnInventoryChanged += InventoryManager_OnInventoryChanged;
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
        if (InventoryManager.instance == null)
        {
            Debug.Log("Inventory Manager not Found!");
            return;
        }

        InventoryManager_OnInventoryChanged(InventoryManager.instance.inventory);
    }


    private void InventoryManager_OnInventoryChanged(Inventory Inventory)
    {
        if (inventory == Inventory)
            return;

        if (inventory != null)
        {
            Inventory.OnItemAdd -= Inventory_OnItemAdd;
            Inventory.OnItemRemove -= Inventory_OnItemRemove;
        }

        inventory = Inventory;

        inventory.OnItemAdd += Inventory_OnItemAdd;
        inventory.OnItemRemove += Inventory_OnItemRemove;
        UpdateVisual();
    }

    private void Inventory_OnItemRemove(Item Item)
    {
        if (itemQualityDictionary.TryGetValue(Item, out ItemQuality itemQuality))
        {
            Destroy(itemQuality.gameObject);
            itemQualityDictionary.Remove(Item);
        }
    }

    private void Inventory_OnItemAdd(Item Item)
    {
        Transform Panel = GetPanel(Item.GetTypeSO());

        if (Panel == null)
            return;

        ItemQuality ItemQualityIEntity = ItemAssetManagerSO.CreateItemQualityItem(Item, Panel.transform);
        ItemQualityIEntity.OnItemQualitySelect += OnSelectedItemQuality;
        itemQualityDictionary.Add(Item, ItemQualityIEntity);
    }

    private void OnSelectedItemQuality(ItemQuality itemQuality)
    {
        OnItemQualitySelect?.Invoke(itemQuality);
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
        InventoryManager.OnInventoryChanged -= InventoryManager_OnInventoryChanged;

        if (inventory != null)
        {
            inventory.OnItemAdd -= Inventory_OnItemAdd;
            inventory.OnItemRemove -= Inventory_OnItemRemove;
        }
    }
}
