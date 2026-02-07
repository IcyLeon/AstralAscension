using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

[CreateAssetMenu(fileName = "ItemManagerSO", menuName = "ScriptableObjects/ItemManager/ItemManagerSO")]
public class ItemManagerSO : ScriptableObject
{
    [field: SerializeField] public GameObject ItemQualityUpgradablePrefab { get; private set; }
    [field: SerializeField] public GameObject ItemQualityBasePrefab { get; private set; }
    [field: SerializeField] public GameObject StarPrefab { get; private set; }

    public ItemQuality CreateItemQualityObject(IData iData, Transform ParentTransform)
    {
        ItemQualityDisplayData manager = ItemQualityDisplayDataManager.Create(iData);
        manager.SetItemManagerSO(this);
        return manager.CreateItemQualityObject(ParentTransform);
    }
}
