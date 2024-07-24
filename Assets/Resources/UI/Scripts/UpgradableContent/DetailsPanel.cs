using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetailsPanel : MonoBehaviour
{
    [SerializeField] private ItemCard itemCard;
    private UpgradableItemContent UpgradableItemContent;

    public IItem iItem { get; private set; }

    private void Awake()
    {
        Init();
    }

    public void UpdateVisual()
    {
        Init();
        iItem = UpgradableItemContent.iItem;
        itemCard.SetInterfaceItem(iItem);
    }

    private void Init()
    {
        if (UpgradableItemContent != null)
            return;

        UpgradableItemContent = GetComponentInParent<UpgradableItemContent>();
    }
}
