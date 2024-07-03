using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemContentDisplay : MonoBehaviour
{
    private ObjectPool<MonoBehaviour> starPool;
    [SerializeField] private ItemManagerSO ItemManagerSO;

    [Header("Base Item Information")]
    [SerializeField] private TextMeshProUGUI ItemNameTxt;
    [SerializeField] private TextMeshProUGUI ItemTypeTxt;
    [SerializeField] private TextMeshProUGUI ItemDescTxt;
    [SerializeField] private Transform StarContainerTransform;

    [Header("Upgradable Item Information")]
    [SerializeField] private GameObject LevelContent;
    [SerializeField] private TextMeshProUGUI LevelTxt;

    private IItem iItem;


    // Update is called once per frame
    private void UpdateVisuals()
    {
        if (iItem == null)
            return;

        UpdateStars();
        ItemNameTxt.text = iItem.GetItemName();
        ItemTypeTxt.text = iItem.GetItemType();
        ItemDescTxt.text = iItem.GetItemDescription();
    }

    private void UpdateStars()
    {
        if (StarContainerTransform == null)
            return;

        if (starPool == null)
            starPool = new ObjectPool<MonoBehaviour>(ItemManagerSO.StarPrefab, StarContainerTransform, 5);

        starPool.ResetAll();
        for (int i = 0; i < (int)iItem.GetItemRarity(); i++)
        {
            starPool.GetPooledObject();
        }
    }
    public void SetInterfaceItem(IItem iItem)
    {
        this.iItem = iItem;
        UpdateVisuals();
    }
}
