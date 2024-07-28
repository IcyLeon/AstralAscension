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
    [SerializeField] private TextMeshProUGUI Artifact2PieceTxt;
    [SerializeField] private TextMeshProUGUI Artifact4PieceTxt;

    private IItem iItem;

    private ItemContentInformation[] ItemContentInformations;

    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        InitStarPool();

        if (ItemContentInformations == null)
        {
            ItemContentInformations = GetComponentsInChildren<ItemContentInformation>(true);
        }
    }

    // Update is called once per frame
    private void UpdateVisual()
    {
        if (iItem == null)
            return;

        UpdateUpgradableItemsVisual();
        UpdateStars();
        UpdateArtifactsSOVisual();

        ItemNameTxt.text = iItem.GetName();
        ItemTypeTxt.text = iItem.GetTypeSO().ItemType;
        ItemDescTxt.text = iItem.GetDescription();

        if (ItemImage)
            ItemImage.sprite = iItem.GetIcon();

        OnItemContentDisplayChanged?.Invoke(this, new ItemContentEvent
        {
            iItem = iItem,
        });
    }


    private void UpdateUpgradableItemsVisual()
    {
        foreach (var ItemContentInformation in ItemContentInformations)
        {
            ItemContentInformation.UpdateItemContentInformation(iItem);
        }

        UpgradableItems UpgradableItems = iItem as UpgradableItems;
        LevelContent.SetActive(UpgradableItems != null);

        UpgradableItemLockItem.SetUpgradableItem(UpgradableItems);

        if (ItemEquipDisplay != null)
            ItemEquipDisplay.UpdateVisual(UpgradableItems);

        if (UpgradableItems == null)
            return;

        LevelTxt.text = "+" + UpgradableItems.amount;
    }

    private void UpdateArtifactsSOVisual()
    {
        ArtifactSO artifactSO = iItem.GetInterfaceItemReference() as ArtifactSO;

        ArtifactContent.SetActive(artifactSO != null);

        ArtifactFamilySO artifactFamilySO = ArtifactManager.instance.ArtifactManagerSO.GetArtifactFamilySO(artifactSO);

        if (artifactFamilySO == null)
            return;

        ArtifactSetTxt.text = artifactFamilySO.ArtifactSetName + ":";
        Artifact2PieceTxt.text = "2-Piece Set: " + artifactFamilySO.TwoPieceDescription;
        Artifact4PieceTxt.text = "4-Piece Set: " + artifactFamilySO.FourPieceDescription;
    }

    private void InitStarPool()
    {
        if (starPool != null || StarContainerTransform == null)
            return;

        starPool = new ObjectPool<MonoBehaviour>(ItemManagerSO.StarPrefab, StarContainerTransform, 5);
    }

    private void UpdateStars()
    {
        if (StarContainerTransform == null)
            return;

        InitStarPool();
        starPool.ResetAll();
        for (int i = 0; i <= (int)iItem.GetRarity(); i++)
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
        UpdateVisual();
    }

    private void OnDestroy()
    {
        UnsubscribeItemEvent();
    }
}
