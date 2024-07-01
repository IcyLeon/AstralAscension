using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using static ItemManager;

public class ItemQuality : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image ItemBackgroundImage;
    [SerializeField] private Image ItemImage;
 
    private IItem IItem;
    public event EventHandler OnItemQualityClick;

    private void Start()
    {
        if (instance == null)
            Debug.LogError("ItemManager not found!");
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnItemQualityClick?.Invoke(this, EventArgs.Empty);
    }

    public void SetItem(IItem item)
    {
        IItem = item;

        if (IItem == null)
            return;

        ItemRaritySO itemRaritySO = instance.ItemManagerSO.GetItemRarityInfomation(IItem.GetItemRarity());

        if (itemRaritySO == null)
            return;

        ItemBackgroundImage.sprite = itemRaritySO.ItemQualityBackground;
        ItemImage.sprite = IItem.GetItemIcon();
    }
}
