using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[DisallowMultipleComponent]
public class ItemQuality : MonoBehaviour
{
    private ObjectPool<MonoBehaviour> starPool;
    [SerializeField] private Transform StarContainer;

    [SerializeField] private TextMeshProUGUI DisplayText;
    [SerializeField] private ItemManagerSO ItemManagerSO;
    [SerializeField] private Image ItemBackgroundImage;
    [SerializeField] private Image ItemImage;

    public IItem iItem { get; private set; }

    private void Awake()
    {
        InitStarPool();
    }

    public void SetInterfaceItem(IItem IItem)
    {
        iItem = IItem;
        UpdateVisuals();
    }

    private void InitStarPool()
    {
        if (starPool != null)
            return;

        starPool = new ObjectPool<MonoBehaviour>(ItemManagerSO.StarPrefab, StarContainer, 5);
    }

    private void UpdateVisuals()
    {
        if (iItem == null)
            return;

        ItemRaritySO itemRaritySO = ItemManagerSO.GetItemRarityInfomation(iItem.GetItemRarity());

        if (itemRaritySO == null)
            return;

        InitStarPool();

        starPool.ResetAll();
        for (int i = 0; i < (int)itemRaritySO.Rarity; i++)
        {
            starPool.GetPooledObject();
        }

        ItemBackgroundImage.sprite = itemRaritySO.ItemQualityBackground;
        ItemImage.sprite = iItem.GetItemIcon();
    }

    public void UpdateDisplayText(string txt)
    {
        DisplayText.text = txt;
    }
}
