using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CharacterEquipmentManager
{
    public CharactersSO charactersSO { get; }

    private Dictionary<Type, CharacterEquipment> _equipmentMap = new();
    public ArtifactEquipment artifactEquipment { get; private set; }
    public EffectManager effectManager { get; private set; }

    public CharacterEquipmentManager(CharactersSO CharactersSO)
    {
        charactersSO = CharactersSO;
        artifactEquipment = new ArtifactEquipment();

        _equipmentMap[typeof(Artifact)] = artifactEquipment;
    }

    public void SetEffectManager(EffectManager effectManager)
    {
        this.effectManager = effectManager;
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
        CharacterEquipment equipment = GetEquipmentMap(UpgradableItems);
        equipment.UnEquip(UpgradableItems);
    }

    private void UpgradableItem_OnEquip(UpgradableItems UpgradableItems)
    {
        CharacterEquipment equipment = GetEquipmentMap(UpgradableItems);
        equipment.Equip(UpgradableItems);
    }

    private CharacterEquipment GetEquipmentMap(UpgradableItems UpgradableItems)
    {
        if (_equipmentMap.TryGetValue(UpgradableItems.GetType(), out var equipment))
        {
            return equipment;
        }

        return null;
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

    public IData GetItem(UpgradableItems UpgradableItems)
    {
        return GetEquipmentMap(UpgradableItems).GetItem(UpgradableItems);
    }
}
