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

    public void UnequipItem(UpgradableItems UpgradableItem)
    {
        if (UpgradableItem == null)
            return;

        UpgradableItem.Unequip();
    }

    private void UpgradableItem_OnUnEquip(UpgradableItems UpgradableItems)
    {
        CharacterEquipment equipment = GetEquipmentMap(UpgradableItems.GetType());
        equipment.UnEquip(UpgradableItems);
    }

    private void UpgradableItem_OnEquip(UpgradableItems UpgradableItems)
    {
        CharacterEquipment equipment = GetEquipmentMap(UpgradableItems.GetType());
        equipment.Equip(UpgradableItems);
    }

    private CharacterEquipment GetEquipmentMap(Type Type)
    {
        if (_equipmentMap.TryGetValue(Type, out var equipment))
        {
            return equipment;
        }

        return null;
    }

    public void EquipItem(UpgradableItems UpgradableItem)
    {
        UpgradableItem.OnEquip += UpgradableItem_OnEquip;
        UpgradableItem.OnUnEquip += UpgradableItem_OnUnEquip;
        UpgradableItem.Equip(charactersSO);
    }

    public IData GetExistingItem(UpgradableItems UpgradableItems)
    {
        return GetEquipmentMap(UpgradableItems.GetType()).GetExistingItem(UpgradableItems);
    }
}
