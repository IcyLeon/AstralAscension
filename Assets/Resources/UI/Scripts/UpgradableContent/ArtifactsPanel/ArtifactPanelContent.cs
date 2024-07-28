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

    private Dictionary<Item, ItemQualityButton> itemQualityDictionary;
    private Inventory inventory;

    private void Awake()
    {
        TabGroup.OnTabGroupChanged += TabGroup_OnTabGroupChanged;
        OnInventoryOld += InventoryManager_OnInventoryOld;
        OnInventoryNew += InventoryManager_OnInventoryNew;
    }

    private void Init()
    {
        if (itemQualityDictionary != null)
        {
            return;
        }

        itemQualityDictionary = new();

        if (instance == null)
        {
            Debug.LogError("Inventory Manager not found!");
            return;
        }

        InventoryManager_OnInventoryNew(instance.inventory);
    }

    private void Start()
    {
        Init();
    }

    private void TabGroup_OnTabGroupChanged(object sender, TabOption.TabEvents e)
    {
        ScrollRect.content = e.PanelRectTransform;
    }

    private void InventoryManager_OnInventoryOld(Inventory Inventory)
    {
        Inventory.OnItemAdd -= Inventory_OnItemAdd;
        Inventory.OnItemRemove -= Inventory_OnItemRemove;
    }

    private void InventoryManager_OnInventoryNew(Inventory Inventory)
    {
        inventory = Inventory;
        if (inventory != null)
        {
            inventory.OnItemAdd += Inventory_OnItemAdd;
            inventory.OnItemRemove += Inventory_OnItemRemove;
            UpdateVisual();
        }
    }

    private void Inventory_OnItemRemove(Item item)
    {
        if (itemQualityDictionary.TryGetValue(item, out ItemQualityButton itemQualityButton))
        {
            itemQualityButton.OnItemQualityClick -= OnSelectedItemQualityClick;
            Destroy(itemQualityButton.gameObject);
            itemQualityDictionary.Remove(item);
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

    private void Inventory_OnItemAdd(Item item)
    {
        Artifact artifact = item as Artifact;

        if (artifact == null || itemQualityDictionary.ContainsKey(artifact))
            return;

        GameObject Panel = GetPanel(artifact.GetTypeSO());

        if (Panel == null)
            return;

        ItemQualityItem itemQualityItem = ItemContentDisplay.ItemManagerSO.CreateItemQualityItem(item, Panel.transform);
        itemQualityItem.OnItemQualityClick += OnSelectedItemQualityClick;
        itemQualityDictionary.Add(item, itemQualityItem);
    }

    private void OnSelectedItemQualityClick(object sender, EventArgs e)
    {
        ItemQualityButton ItemQualityButton = sender as ItemQualityButton;
        ItemContentDisplay.SetInterfaceItem(ItemQualityButton.ItemQuality.iItem);
    }

    private void UpdateVisual()
    {
        for (int i = itemQualityDictionary.Count - 1; i >= 0; i--)
        {
            Inventory_OnItemRemove(itemQualityDictionary.ElementAt(i).Key);
        }

        itemQualityDictionary.Clear();

        foreach (var item in inventory.itemList)
        {
            Inventory_OnItemAdd(item);
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
