using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private ItemContentDisplay ItemContentDisplay;
    [SerializeField] private Image CardImage;

    public void SetInterfaceItem(IItem iItem)
    {
        if (iItem == null)
            return;

        ItemRaritySO itemRaritySO = ItemContentDisplay.ItemManagerSO.GetItemRarityInfomation(iItem.GetItemRarity());
        if (itemRaritySO == null)
            return;

        CardImage.sprite = itemRaritySO.ItemCardBackground;
        ItemContentDisplay.SetInterfaceItem(iItem);
    }
}
