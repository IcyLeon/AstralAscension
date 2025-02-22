using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpgradeAction : MonoBehaviour
{
    private ItemContentDisplay itemContentDisplay;
    private UpgradeItemContent upgradeItemContent;
    private UpgradableItems upgradableItem;
    private Button upgradeBtn;
    private MainUI mainUI;

    private void Awake()
    {
        mainUI = GetComponentInParent<MainUI>();

        itemContentDisplay = GetComponentInParent<ItemContentDisplay>();
        itemContentDisplay.OnItemContentDisplayChanged += ItemContentDisplay_OnItemContentDisplayChanged;

        upgradeBtn = GetComponent<Button>();
        upgradeBtn.onClick.AddListener(OnUpgrade);

        UpdateVisuals();
    }

    private void Start()
    {
        upgradeItemContent = mainUI.UpgradeItemContent;
    }

    private void ItemContentDisplay_OnItemContentDisplayChanged()
    {
        upgradableItem = itemContentDisplay.iItem as UpgradableItems;
        UpdateVisuals();
    }

    // Update is called once per frame
    private void OnUpgrade()
    {
        upgradeItemContent.SetUpgradableItem(upgradableItem);
    }

    private void UpdateVisuals()
    {
        gameObject.SetActive(upgradableItem != null);
    }

    private void OnDestroy()
    {
        itemContentDisplay.OnItemContentDisplayChanged -= ItemContentDisplay_OnItemContentDisplayChanged;
        upgradeBtn.onClick.RemoveAllListeners();
    }
}
