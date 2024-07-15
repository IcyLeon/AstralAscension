using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradableItemContent : MonoBehaviour
{
    [SerializeField] private ItemCard itemCard;
    private IItem iItem;

    public void SetIItem(IItem IItem)
    {
        iItem = IItem;
        gameObject.SetActive(iItem != null);
        itemCard.SetInterfaceItem(iItem);
    }
}
