using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static AssetManager;

public class ItemCard : MonoBehaviour
{
    [SerializeField] private ItemContentDisplay ItemContentDisplay;
    [SerializeField] private Image CardImage;
    private ItemManagerSO ItemAssetManagerSO;

    private void Start()
    {
        InitAssetManager();
    }

    private void InitAssetManager()
    {
        if (ItemAssetManagerSO != null)
            return;

        ItemAssetManagerSO = instance.ItemAssetManagerSO;
    }

    public void SetIItem(IItem iItem)
    {
        gameObject.SetActive(iItem != null);

        if (iItem == null)
            return;

        InitAssetManager();

        ItemRaritySO itemRaritySO = iItem.GetRaritySO();

        CardImage.sprite = itemRaritySO.ItemCardBackground;
        ItemContentDisplay.SetIItem(iItem);
    }
}
