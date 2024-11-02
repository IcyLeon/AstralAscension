using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;
using static AssetManager;

[DisallowMultipleComponent]
public class ItemQuality : MonoBehaviour
{
    private ObjectPool<MonoBehaviour> starPool;
    private ItemManagerSO ItemAssetManagerSO;

    [SerializeField] private Transform StarContainer;

    [SerializeField] private TextMeshProUGUI DisplayText;
    [SerializeField] private Image ItemBackgroundImage;
    [SerializeField] private Image ItemImage;

    public IItem iItem { get; private set; }

    private void Awake()
    {
        InitAssets();
    }

    private void InitAssets()
    {
        InitAssetManager();
        InitStarPool();
    }

    private void InitAssetManager()
    {
        if (ItemAssetManagerSO != null)
            return;

        ItemAssetManagerSO = instance.ItemAssetManagerSO;
    }

    public void SetIItem(IItem IItem)
    {
        iItem = IItem;
        UpdateVisual();
    }

    private void InitStarPool()
    {
        if (starPool != null)
            return;

        starPool = new ObjectPool<MonoBehaviour>(ItemAssetManagerSO.StarPrefab, StarContainer, 5);
    }

    private void UpdateVisual()
    {
        if (iItem == null)
            return;

        InitAssets();
        starPool.ResetAll();

        ItemRaritySO itemRaritySO = ItemAssetManagerSO.GetItemRarityInfomation(iItem.GetRarity());

        for (int i = 0; i <= (int)iItem.GetRarity(); i++)
        {
            starPool.GetPooledObject();
        }

        ItemBackgroundImage.sprite = itemRaritySO.ItemQualityBackground;
        ItemImage.sprite = iItem.GetIcon();
    }

    public void UpdateDisplayText(string txt)
    {
        DisplayText.text = txt;
    }
}
