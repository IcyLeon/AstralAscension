using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static AssetManager;

public class ItemContentDisplay : MonoBehaviour
{

    private ObjectPool<MonoBehaviour> starPool;
    private ItemManagerSO ItemAssetManagerSO;
    public event Action OnItemContentDisplayChanged;

    [Header("Base Item Information")]
    [SerializeField] private TextMeshProUGUI ItemNameTxt;
    [SerializeField] private TextMeshProUGUI ItemTypeTxt;
    [SerializeField] private TextMeshProUGUI ItemDescTxt;
    [SerializeField] private Image ItemImage;
    [SerializeField] private Transform StarContainerTransform;

    [Header("Upgradable Item Information")]
    [SerializeField] private GameObject LevelContent;
    [SerializeField] private LockItem LockItem;
    [SerializeField] private TextMeshProUGUI LevelTxt;

    public IData iData { get; private set; }

    private ItemContentInformation[] ItemContentInformations;

    private void Start()
    {
        ItemAssetManagerSO = instance.ItemAssetManagerSO;
        Init();
    }

    private void Init()
    {
        if (ItemContentInformations != null)
            return;

        ItemContentInformations = GetComponentsInChildren<ItemContentInformation>(true);
        InitStarPool();
    }

    // Update is called once per frame
    private void UpdateSubInformationVisuals()
    {
        if (iData == null)
            return;

        foreach (var ItemContentInformation in ItemContentInformations)
        {
            ItemContentInformation.UpdateItemContentInformation(iData);
        }
    }


    private void UpdateIItemVisual()
    {
        if (iData == null)
            return;

        UpdateStars();

        LockItem.SetIItem(iData);

        ItemNameTxt.text = iData.GetName();
        ItemTypeTxt.text = iData.GetTypeSO().ItemType;
        ItemDescTxt.text = iData.GetDescription();

        if (ItemImage)
            ItemImage.sprite = iData.GetIcon();

        OnItemContentDisplayChanged?.Invoke();
    }

    private void UpdateUpgradableItemsVisual()
    {
        UpgradableItems UpgradableItems = iData as UpgradableItems;
        LevelContent.SetActive(UpgradableItems != null);

        if (UpgradableItems == null)
            return;

        LevelTxt.text = "+" + UpgradableItems.level;
    }

    private void InitStarPool()
    {
        if (starPool != null || StarContainerTransform == null)
            return;

        starPool = new ObjectPool<MonoBehaviour>(ItemAssetManagerSO.StarPrefab, StarContainerTransform, 5);
    }

    private void UpdateStars()
    {
        if (StarContainerTransform == null)
            return;

        InitStarPool();
        starPool.ResetAll();

        ItemRaritySO itemRaritySO = iData.GetRaritySO();

        for (int i = 0; i <= (int)itemRaritySO.Rarity; i++)
        {
            starPool.GetPooledObject();
        }
    }
    public void SetIItem(IData IData)
    {
        if (iData == IData)
            return;

        Init();
        UnsubscribeItemEvent();
        iData = IData;
        UpdateIItemVisual();
        UpdateSubInformationVisuals();

        SubscribeItemEvent();
    }

    private void UnsubscribeItemEvent()
    {
        IEntity iEntity = iData as IEntity;

        if (iEntity == null)
            return;

        iEntity.OnIEntityChanged -= IEntity_OnIEntityChanged;
        iEntity.OnIEntityChanged -= UpgradableItem_OnIEntityChanged;
    }

    private void SubscribeItemEvent()
    {
        IEntity iEntity = iData as IEntity;

        if (iEntity == null)
            return;

        iEntity.OnIEntityChanged += IEntity_OnIEntityChanged;
        iEntity.OnIEntityChanged += UpgradableItem_OnIEntityChanged;

        UpdateUpgradableItemsVisual();
    }

    private void UpgradableItem_OnIEntityChanged(IEntity e)
    {
        UpdateUpgradableItemsVisual();
    }

    private void IEntity_OnIEntityChanged(IEntity e)
    {
        UpdateSubInformationVisuals();
    }

    private void OnDestroy()
    {
        UnsubscribeItemEvent();
    }
}
