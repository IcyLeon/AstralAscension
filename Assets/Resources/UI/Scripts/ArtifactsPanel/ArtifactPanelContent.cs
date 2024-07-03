using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static InventoryManager;

public class ItemQualityButtonEvent : EventArgs
{
    public ItemQualityButton selectedItemQualityButton;
}


[DisallowMultipleComponent]
public class ArtifactPanelContent : MonoBehaviour
{
    [Serializable]
    public class ArtifactPanel
    {
        [field: SerializeField] public GameObject Panel { get; private set; }
        [field: SerializeField] public ItemTypeSO ItemTypeSO { get; private set; }
    }

    [SerializeField] private TabGroup TabGroup;
    [SerializeField] private ArtifactPanel[] ArtifactPanelList;

    public event EventHandler<ItemQualityButtonEvent> OnItemQualityButtonChanged;

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
            itemQualityButton.OnItemQualityClick -= ItemQualityItem_OnItemQualityClick;
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

        GameObject Panel = GetPanel(artifact.artifactSO.ItemTypeSO);

        if (Panel == null)
            return;

        ItemQualityItem itemQualityItem = instance.ItemManagerSO.CreateItemQualityItem(item, Panel.transform);
        itemQualityItem.OnItemQualityClick += ItemQualityItem_OnItemQualityClick;
        itemQualityDictionary.Add(item, itemQualityItem);
    }

    private void ItemQualityItem_OnItemQualityClick(object sender, EventArgs e)
    {
        OnItemQualityButtonChanged?.Invoke(this, new ItemQualityButtonEvent { selectedItemQualityButton = sender as ItemQualityButton });
    }

    private void UpdateVisuals()
    {
        foreach(var itemQualityKeyPairs in itemQualityDictionary)
        {
            itemQualityKeyPairs.Value.OnItemQualityClick -= ItemQualityItem_OnItemQualityClick;
            Destroy(itemQualityKeyPairs.Value.gameObject);
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
