using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemManagerSO", menuName = "ScriptableObjects/ItemManager/ItemManagerSO")]
public class ItemManagerSO : ScriptableObject
{
    [SerializeField] private GameObject ItemQualityItemPrefab;
    [field: SerializeField] public GameObject StarPrefab { get; private set; }
    [field: SerializeField] public ItemRaritySO[] ItemRaritySOList { get; private set; }

    public ItemRaritySO GetItemRarityInfomation(Rarity rarity)
    {
        foreach(var itemRarity in ItemRaritySOList)
        {
            if (itemRarity.Rarity == rarity)
                return itemRarity;
        }
        return null;
    }

    public ItemQualityItem CreateItemQualityItem(IEntity IEntity, Transform ParentTransform)
    {
        ItemQualityItem itemQualityItem = Instantiate(ItemQualityItemPrefab, ParentTransform).GetComponent<ItemQualityItem>();
        itemQualityItem.SetIItem(IEntity);
        return itemQualityItem;
    }

}
