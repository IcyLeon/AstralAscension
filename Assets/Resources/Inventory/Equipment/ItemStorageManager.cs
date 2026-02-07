using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStorageManager : MonoBehaviour
{
    [SerializeField] private Transform ParentTransform;
    protected Dictionary<IData, ItemQuality> itemList { get; private set; } = new();
 
    protected void ResetList()
    {
        foreach (var skinSO in itemList.Keys)
        {
            ItemQuality itemQuality = itemList[skinSO];
            itemQuality.OnItemQualitySelect -= ItemQuality_OnItemQualitySelect;
            Destroy(itemQuality.gameObject);
        }

        itemList.Clear();
    }

    protected virtual void Start()
    {

    }

    protected void AddItem(IData IData)
    {
        if (IData == null || itemList.ContainsKey(IData))
            return;

        ItemQuality itemQuality = AssetManager.instance.ItemAssetManagerSO.CreateItemQualityObject(IData, ParentTransform);
        itemQuality.OnItemQualitySelect += ItemQuality_OnItemQualitySelect;
        itemList.Add(IData, itemQuality);
    }

    protected void SelectItemQuality(IData IData)
    {
        if (IData == null || !itemList.ContainsKey(IData))
            return;

        ItemQuality itemQuality = itemList[IData];
        itemQuality.Select();
    }

    protected virtual void OnDestroy()
    {
        ResetList();
    }

    protected virtual void ItemQuality_OnItemQualitySelect(IData IData)
    {

    }
}
