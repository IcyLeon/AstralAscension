using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemContentDisplay : MonoBehaviour
{
    public class ItemContentEvent : EventArgs
    {
        public IItem iItem;
    }

    private ObjectPool<MonoBehaviour> starPool;

    public event EventHandler<ItemContentEvent> OnItemContentDisplayChanged;

    [field: SerializeField] public ItemManagerSO ItemManagerSO { get; private set; }

    [Header("Base Item Information")]
    [SerializeField] private TextMeshProUGUI ItemNameTxt;
    [SerializeField] private TextMeshProUGUI ItemTypeTxt;
    [SerializeField] private TextMeshProUGUI ItemDescTxt;
    [SerializeField] private Image ItemImage;
    [SerializeField] private Transform StarContainerTransform;

    [Header("Upgradable Item Information")]
    [SerializeField] private GameObject LevelContent;
    [SerializeField] private LockItem UpgradableItemLockItem;
    [SerializeField] private TextMeshProUGUI LevelTxt;
    [SerializeField] private ItemEquipDisplay ItemEquipDisplay;

    [Header("Artifact Item Information")]
    [SerializeField] private GameObject ArtifactContent;
    [SerializeField] private TextMeshProUGUI ArtifactSetTxt;

    private IItem iItem;

    private ArtifactSubStatDisplay[] artifactSubStatDisplayList;
    private ArtifactMainStatDisplay artifactMainStatDisplay;

    private void Start()
    {
        Init();
    }

    private void Init()
    {
        InitStarPool();

        if (artifactSubStatDisplayList == null)
            artifactSubStatDisplayList = GetComponentsInChildren<ArtifactSubStatDisplay>(true);

        if (artifactMainStatDisplay == null)
            artifactMainStatDisplay = GetComponentInChildren<ArtifactMainStatDisplay>(true);
    }

    // Update is called once per frame
    private void UpdateVisuals()
    {
        if (iItem == null)
            return;

        UpdateUpgradableItemsVisual();
        UpdateStars();
        UpdateArtifactsSOVisual();

        ItemNameTxt.text = iItem.GetItemName();
        ItemTypeTxt.text = iItem.GetItemType().ItemType;
        ItemDescTxt.text = iItem.GetItemDescription();

        if (ItemImage)
            ItemImage.sprite = iItem.GetItemIcon();

        OnItemContentDisplayChanged?.Invoke(this, new ItemContentEvent
        {
            iItem = iItem,
        });
    }


    private void UpdateUpgradableItemsVisual()
    {
        UpgradableItems UpgradableItems = iItem as UpgradableItems;
        LevelContent.SetActive(UpgradableItems != null);

        UpgradableItemLockItem.SetUpgradableItem(UpgradableItems);

        if (ItemEquipDisplay != null)
            ItemEquipDisplay.UpdateVisual(UpgradableItems);

        UpdateArtifactStatsDisplay();

        if (UpgradableItems == null)
            return;

        LevelTxt.text = "+" + UpgradableItems.amount;
    }

    private void UpdateArtifactStatsDisplay()
    {
        Artifact artifact = iItem as Artifact;

        if (artifactMainStatDisplay != null)
            artifactMainStatDisplay.SetArtifactItem(artifact);

        for (int i = 0; i < artifactSubStatDisplayList.Length; i++)
        {
            ArtifactSubStatDisplay artifactSubStatDisplay = artifactSubStatDisplayList[i];
            artifactSubStatDisplay.gameObject.SetActive(false);
            artifactSubStatDisplay.SetIndex(i);
            artifactSubStatDisplay.SetArtifactItem(artifact);
        }
    }

    private void UpdateArtifactsSOVisual()
    {
        ArtifactSO artifactSO = iItem.GetInterfaceItemReference() as ArtifactSO;

        ArtifactContent.SetActive(artifactSO != null);

        if (artifactSO == null)
            return;

        ArtifactFamilySO artifactFamilySO = ArtifactManager.instance.ArtifactManagerSO.GetArtifactFamilySO(artifactSO);
        string ArtifactSetName = "???";
        if (artifactFamilySO != null)
        {
            ArtifactSetName = artifactFamilySO.ArtifactSetName;
        }
        ArtifactSetTxt.text = ArtifactSetName + ":";
    }

    private void InitStarPool()
    {
        if (starPool != null)
            return;

        starPool = new ObjectPool<MonoBehaviour>(ItemManagerSO.StarPrefab, StarContainerTransform, 5);
    }

    private void UpdateStars()
    {
        if (StarContainerTransform == null)
            return;

        InitStarPool();
        starPool.ResetAll();
        for (int i = 0; i < (int)iItem.GetItemRarity(); i++)
        {
            starPool.GetPooledObject();
        }
    }
    public void SetInterfaceItem(IItem IItem)
    {
        if (iItem == IItem)
            return;

        Init();
        UnsubscribeItemEvent();
        iItem = IItem;
        SubscribeItemEvent();

        UpdateVisuals();
    }

    private void UnsubscribeItemEvent()
    {
        Item item = iItem as Item;
        if (item == null)
            return;

        item.OnItemChanged -= Item_OnItemChanged;
    }

    private void Item_OnItemChanged(object sender, System.EventArgs e)
    {
        UpdateUpgradableItemsVisual();
    }

    private void SubscribeItemEvent()
    {
        Item item = iItem as Item;
        if (item == null)
            return;

        item.OnItemChanged += Item_OnItemChanged;
    }

    private void OnDestroy()
    {
        UnsubscribeItemEvent();
    }
}
