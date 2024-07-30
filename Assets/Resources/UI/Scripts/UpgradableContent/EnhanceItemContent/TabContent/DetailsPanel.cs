using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DetailsPanel : MonoBehaviour
{
    [SerializeField] private ItemCard ItemCard;
    [SerializeField] private Image EquipCharacterIconImage;
    private EnhanceItemContent EnhanceItemContent;

    public IItem iItem { get; private set; }

    private void Awake()
    {
        Init();
    }

    public void UpdateVisual()
    {
        Init();
        iItem = EnhanceItemContent.iItem;
        ItemCard.SetInterfaceItem(iItem);
        UpdateUpgradableItemVisual();
    }

    private void UpdateUpgradableItemVisual()
    {
        UpgradableItems upgradableItems = iItem as UpgradableItems;

        EquipCharacterIconImage.gameObject.SetActive(upgradableItems != null && upgradableItems.equipByCharacter != null);

        if (!EquipCharacterIconImage.gameObject.activeSelf)
            return;

        EquipCharacterIconImage.sprite = upgradableItems.equipByCharacter.GetIcon();
    }

    private void Init()
    {
        if (EnhanceItemContent != null)
            return;

        EnhanceItemContent = GetComponentInParent<EnhanceItemContent>();
    }
}
