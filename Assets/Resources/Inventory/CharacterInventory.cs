using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterInventory
{
    public CharactersSO charactersSO { get; }
    private Dictionary<ItemTypeSO, Item> itemList;
    public ArtifactInventory artifactInventory { get; private set; }
    public delegate void OnItemEquippedEvent(UpgradableItems UpgradableItem);
    public event OnItemEquippedEvent OnEquip;
    public event OnItemEquippedEvent OnUnEquip;

    public CharacterInventory(CharactersSO CharactersSO)
    {
        itemList = new();
        charactersSO = CharactersSO;
        artifactInventory = new ArtifactInventory(this);
    }

    public void UnequipItem(Item Item)
    {
        UpgradableItems upgradableItem = Item as UpgradableItems;

        if (upgradableItem == null)
            return;

        upgradableItem.Unequip();
    }

    private void UpgradableItem_OnUnEquip(UpgradableItems UpgradableItems)
    {
        itemList.Remove(UpgradableItems.GetTypeSO());
        OnUnEquip?.Invoke(UpgradableItems);
    }

    private void UpgradableItem_OnEquip(UpgradableItems UpgradableItems)
    {
        itemList.Add(UpgradableItems.GetTypeSO(), UpgradableItems);
        OnEquip?.Invoke(UpgradableItems);
    }

    public void EquipItem(Item Item)
    {
        UpgradableItems upgradableItem = Item as UpgradableItems;

        if (upgradableItem == null)
            return;

        upgradableItem.OnEquip += UpgradableItem_OnEquip;
        upgradableItem.OnUnEquip += UpgradableItem_OnUnEquip;
        upgradableItem.Equip(charactersSO);
    }

    public Item GetItem(ItemTypeSO itemTypeSO)
    {
        if (itemList.TryGetValue(itemTypeSO, out Item item))
            return item;

        return null;
    }
}
