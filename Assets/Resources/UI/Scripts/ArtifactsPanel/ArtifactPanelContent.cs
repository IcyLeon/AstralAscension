using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static InventoryManager;

[DisallowMultipleComponent]
public class ArtifactPanelContent : MonoBehaviour
{
    [Serializable]
    public class ArtifactPanel
    {
        [field: SerializeField] public GameObject Panel { get; private set; }
        [field: SerializeField] public ItemTypeSO ItemTypeSO { get; private set; }
    }

    [SerializeField] private ItemContentDisplay ItemContentDisplay;
    [SerializeField] private TabGroup TabGroup;
    [SerializeField] private ArtifactPanel[] ArtifactPanelList;

    private Dictionary<Item, ItemQualityButton> itemQualityDictionary;
    private Inventory Inventory;

    private void Awake()
    {
        itemQualityDictionary = new();
        OnInventoryOld += InventoryManager_OnInventoryOld;
        OnInventoryNew += InventoryManager_OnInventoryNew;
    }

    private void InventoryManager_OnInventoryOld(Inventory inventory)
    {
        if (inventory != null)
        {
            inventory.OnItemAdd -= Inventory_OnItemAdd;
            inventory.OnItemRemove -= Inventory_OnItemRemove;
        }
    }

    private void InventoryManager_OnInventoryNew(Inventory inventory)
    {
        Inventory = inventory;
        if (Inventory != null)
        {
            Inventory.OnItemAdd += Inventory_OnItemAdd;
            Inventory.OnItemRemove += Inventory_OnItemRemove;
            UpdateVisuals();
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
                return ArtifactPanel.Panel;
        }
        return null;
    }

    private void Inventory_OnItemAdd(Item item)
    {
        Artifact artifact = item as Artifact;

        if (artifact == null || itemQualityDictionary.ContainsKey(artifact))
            return;

        GameObject Panel = GetPanel(artifact.GetItemType());

        if (Panel == null)
            return;

        ItemQualityItem itemQualityItem = instance.ItemManagerSO.CreateItemQualityItem(item, Panel.transform);
        itemQualityItem.OnItemQualityClick += OnSelectedItemQualityClick;
        itemQualityDictionary.Add(item, itemQualityItem);
    }

    private void OnSelectedItemQualityClick(object sender, EventArgs e)
    {
        ItemQualityButton ItemQualityButton = sender as ItemQualityButton;
        ItemContentDisplay.SetInterfaceItem(ItemQualityButton.ItemQuality.iItem);
    }

    private void UpdateVisuals()
    {
        foreach(var itemQualityKeyPairs in itemQualityDictionary)
        {
            Inventory_OnItemRemove(itemQualityKeyPairs.Key);
        }

        itemQualityDictionary.Clear();

        foreach (var item in Inventory.itemList)
        {
            Inventory_OnItemAdd(item);
        }
    }

    private void OnDestroy()
    {
        OnInventoryOld -= InventoryManager_OnInventoryOld;
        OnInventoryNew -= InventoryManager_OnInventoryNew;

        if (Inventory != null)
        {
            Inventory.OnItemAdd -= Inventory_OnItemAdd;
            Inventory.OnItemRemove -= Inventory_OnItemRemove;
        }
    }

    // Start is called before the first frame update
    private void Start()
    {
        if (instance == null)
            Debug.LogError("Inventory Manager  not found!");
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
