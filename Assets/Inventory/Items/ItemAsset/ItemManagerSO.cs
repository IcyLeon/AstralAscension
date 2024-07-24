using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemManagerSO", menuName = "ScriptableObjects/ItemManager/ItemManagerSO")]
public class ItemManagerSO : ScriptableObject
{
    [field: SerializeField] public GameObject ItemQualityItemPrefab { get; private set; }
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

    public ItemQualityItem CreateItemQualityItem(Item item, Transform ParentTransform)
    {
        ItemQualityItem itemQualityItem = Instantiate(ItemQualityItemPrefab, ParentTransform).GetComponent<ItemQualityItem>();
        itemQualityItem.SetIEntity(item);
        return itemQualityItem;
    }

}
