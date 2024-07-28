using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[DisallowMultipleComponent]
public class EnhanceStatsPanel : MonoBehaviour
{
    [Header("Upgradable Item Content")]
    [SerializeField] private GameObject LevelContent;
    [SerializeField] private TextMeshProUGUI LevelTxt;

    private IItem iItem;
    private ItemContentInformation[] ItemContentInformations;

    // Start is called before the first frame update
    private void Awake()
    {
        Init();
    }

    private void Init()
    {
        if (ItemContentInformations != null)
            return;

        ItemContentInformations = GetComponentsInChildren<ItemContentInformation>(true);
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

    private void OnDestroy()
    {
        UnsubscribeItemEvent();
    }

    private void UnsubscribeItemEvent()
    {
        Item item = iItem as Item;
        if (item == null)
            return;

        item.OnItemChanged -= Item_OnItemChanged;
    }

    private void UpdateUpgradableItem()
    {
        UpgradableItems upgradableItems = iItem as UpgradableItems;

        LevelContent.gameObject.SetActive(iItem != null);

        if (!LevelContent.activeSelf)
            return;

        LevelTxt.text = "+" + upgradableItems.amount;

    }

    private void SubscribeItemEvent()
    {
        Item item = iItem as Item;
        if (item == null)
            return;

        item.OnItemChanged += Item_OnItemChanged;
        UpdateVisual();
    }

    private void Item_OnItemChanged(object sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        foreach (var ItemContentInformation in ItemContentInformations)
        {
            ItemContentInformation.UpdateItemContentInformation(iItem);
        }

        UpdateUpgradableItem();

    }

    // Update is called once per frame
    private void Update()
    {
        
    }
}
