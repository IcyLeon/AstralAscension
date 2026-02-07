using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = UnityEngine.Object;

public static class ItemQualityDisplayDataManager
{
    private static Dictionary<Type, Func<IData, ItemQualityDisplayData>> BaseMap = new()
    {
        { typeof(Item), data => new ItemQualityItemDisplayData((Item)data) },
        { typeof(UpgradableItems), data => new ItemQualityUpgradableDisplayData((UpgradableItems)data) }
    };

    private static readonly Dictionary<Type, Func<IData, ItemQualityDisplayData>> Cache = new();

    public static ItemQualityDisplayData Create(IData data)
    {
        Type t = data.GetType();
        Type curr = t;

        if (Cache.ContainsKey(t))
            return Cache[t](data);

        while (curr != null)
        {
            if (BaseMap.TryGetValue(curr, out Func<IData, ItemQualityDisplayData> creator))
            {
                Cache.Add(t, creator);
                return creator(data);
            }

            curr = curr.BaseType;
        }

        Cache[t] = d => new ItemQualityDisplayData(d);
        return Cache[t](data);
    }
}


public class ItemQualityDisplayData
{
    public IData iData { get; }
    protected ItemManagerSO itemManagerSO;
    public ItemQualityDisplayData(IData IData)
    {
        iData = IData;
    }

    public void SetItemManagerSO(ItemManagerSO ItemManagerSO)
    {
        itemManagerSO = ItemManagerSO;
    }

    protected virtual GameObject GetPrefab()
    {
        return itemManagerSO.ItemQualityBasePrefab;
    }

    public virtual ItemQuality CreateItemQualityObject(Transform ParentTransform)
    {
        ItemQuality ItemQuality = Object.Instantiate(GetPrefab(), ParentTransform).GetComponent<ItemQuality>();
        ItemQuality.SetIData(this);
        return ItemQuality;
    }

    public virtual string GetDisplayText()
    {
        return iData.GetName();
    }
}

public abstract class ItemQualityIEntityDisplayData : ItemQualityDisplayData
{
    public IEntity iEntity { get; }
    public ItemQualityIEntityDisplayData(IEntity IEntity) : base(IEntity)
    {
        iEntity = IEntity;
    }

    public override ItemQuality CreateItemQualityObject(Transform ParentTransform)
    {
        ItemQuality ItemQuality = base.CreateItemQualityObject(ParentTransform);
        ItemQualityIEntity ItemQualityIEntity = ItemQuality.GetComponent<ItemQualityIEntity>();

        if (ItemQualityIEntity == null)
        {
            Debug.Log("The Prefab do not have ItemQualityIEntity Component");
            return ItemQuality;
        }

        ItemQualityIEntity.SetIData(this);
        return ItemQuality;
    }
}

public class ItemQualityItemDisplayData : ItemQualityIEntityDisplayData
{
    private Item item;
    public ItemQualityItemDisplayData(Item Item) : base(Item)
    {
        item = Item;
    }
    public override string GetDisplayText()
    {
        return item.amount.ToString();
    }
}

public class ItemQualityUpgradableDisplayData : ItemQualityItemDisplayData
{
    private UpgradableItems upgradableItem;
    public ItemQualityUpgradableDisplayData(UpgradableItems UpgradableItem) : base(UpgradableItem)
    {
        upgradableItem = UpgradableItem;
    }

    public override string GetDisplayText()
    {
        return "+" + upgradableItem.level;
    }

    protected override GameObject GetPrefab()
    {
        return itemManagerSO.ItemQualityUpgradablePrefab;
    }
}