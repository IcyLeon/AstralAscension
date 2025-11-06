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

    public ItemQualityIEntity CreateItemQualityItem(IData iData, Transform ParentTransform)
    {
        ItemQualityIEntity itemQualityItem = Instantiate(ItemQualityItemPrefab, ParentTransform).GetComponent<ItemQualityIEntity>();
        itemQualityItem.SetIItem(iData);
        return itemQualityItem;
    }

    public ObjectPool<ItemQualityIEntity> CreateItemQualityItem(Transform ParentTransform, int amt)
    {
        return new ObjectPool<ItemQualityIEntity>(ItemQualityItemPrefab, ParentTransform, amt);
    }

}
