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

    public IItem iItem { get; private set; }

    private ItemContentInformation[] ItemContentInformations;

    private void InitAssetManager()
    {
        if (ItemAssetManagerSO != null)
            return;

        ItemAssetManagerSO = instance.ItemAssetManagerSO;
    }

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        if (ItemContentInformations != null)
            return;

        ItemContentInformations = GetComponentsInChildren<ItemContentInformation>(true);
        InitAssets();
    }

    private void InitAssets()
    {
        InitAssetManager();
        InitStarPool();
    }

    // Update is called once per frame
    private void UpdateSubInformationVisuals()
    {
        if (iItem == null)
            return;

        foreach (var ItemContentInformation in ItemContentInformations)
        {
            ItemContentInformation.UpdateItemContentInformation(iItem);
        }
    }


    private void UpdateIItemVisual()
    {
        if (iItem == null)
            return;

        UpdateStars();

        LockItem.SetIItem(iItem);

        ItemNameTxt.text = iItem.GetName();
        ItemTypeTxt.text = iItem.GetTypeSO().ItemType;
        ItemDescTxt.text = iItem.GetDescription();

        if (ItemImage)
            ItemImage.sprite = iItem.GetIcon();

        OnItemContentDisplayChanged?.Invoke();
    }

    private void UpdateUpgradableItemsVisual()
    {
        UpgradableItems UpgradableItems = iItem as UpgradableItems;
        LevelContent.SetActive(UpgradableItems != null);

        if (UpgradableItems == null)
            return;

        LevelTxt.text = "+" + UpgradableItems.amount;
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

        InitAssets();
        starPool.ResetAll();

        ItemRaritySO itemRaritySO = iItem.GetRaritySO();

        for (int i = 0; i <= (int)itemRaritySO.Rarity; i++)
        {
            starPool.GetPooledObject();
        }
    }
    public void SetIItem(IItem IItem)
    {
        if (iItem == IItem)
            return;

        Init();
        UnsubscribeItemEvent();
        iItem = IItem;
        UpdateIItemVisual();
        UpdateSubInformationVisuals();

        SubscribeItemEvent();
    }

    private void UnsubscribeItemEvent()
    {
        IEntity iEntity = iItem as IEntity;

        if (iEntity == null)
            return;

        iEntity.OnIEntityChanged -= IEntity_OnIEntityChanged;
        iEntity.OnIEntityChanged -= UpgradableItem_OnIEntityChanged;
    }

    private void SubscribeItemEvent()
    {
        IEntity iEntity = iItem as IEntity;

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
