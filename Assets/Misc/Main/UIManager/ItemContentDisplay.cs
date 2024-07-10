using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class ItemContentDisplay : MonoBehaviour
{
    private ObjectPool<MonoBehaviour> starPool;
    private ObjectPool<ArtifactSubStatDisplay> subStatPool;

    [SerializeField] private ItemManagerSO ItemManagerSO;

    [Header("Base Item Information")]
    [SerializeField] private TextMeshProUGUI ItemNameTxt;
    [SerializeField] private TextMeshProUGUI ItemTypeTxt;
    [SerializeField] private TextMeshProUGUI ItemDescTxt;
    [SerializeField] private Transform StarContainerTransform;

    [Header("Upgradable Item Information")]
    [SerializeField] private GameObject LevelContent;
    [SerializeField] private LockItem UpgradableItemLockItem;
    [SerializeField] private TextMeshProUGUI LevelTxt;

    [Header("Artifact Item Information")]
    [SerializeField] private GameObject ArtifactContent;
    [SerializeField] private TextMeshProUGUI ArtifactSetTxt;
    [SerializeField] private Transform SubStatContainerParent;

    private IItem iItem;

    private ArtifactMainStatDisplay artifactMainStatDisplay;

    private void Start()
    {
        InitStarPool();
        subStatPool = new ObjectPool<ArtifactSubStatDisplay>(ArtifactManager.instance.ArtifactManagerSO.SubStatItemContentPrefab, SubStatContainerParent, 5);
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
        UpdateLockVisual();

        ItemNameTxt.text = iItem.GetItemName();
        ItemTypeTxt.text = iItem.GetItemType().ItemType;
        ItemDescTxt.text = iItem.GetItemDescription();
    }

    private void UpdateLockVisual()
    {
        UpgradableItems UpgradableItems = iItem as UpgradableItems;
        UpgradableItemLockItem.SetUpgradableItem(UpgradableItems);
    }

    private void UpdateUpgradableItemsVisual()
    {
        UpgradableItems UpgradableItems = iItem as UpgradableItems;
        LevelContent.SetActive(UpgradableItems != null);
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

        if (subStatPool != null)
        {
            subStatPool.ResetAll();
            subStatPool.CallbackPoolObject((artifactSubStatDisplay, i) =>
                {
                    artifactSubStatDisplay.SetIndex(i);
                    artifactSubStatDisplay.SetArtifactItem(artifact);
                }
            );
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
