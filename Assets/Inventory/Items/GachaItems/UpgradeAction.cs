using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeAction : MonoBehaviour
{
    [SerializeField] private ItemContentDisplay ItemContentDisplay;
    private EnhanceItemContent EnhanceItemContent;
    private UpgradableItems upgradableItems;
    private Button UpgradeBtn;

    private void Awake()
    {
        ItemContentDisplay.OnItemContentDisplayChanged += ItemContentDisplay_OnItemContentDisplayChanged;
        UpdateVisual();

        UpgradeBtn = GetComponent<Button>();

        if (UpgradeBtn == null)
            return;

        UpgradeBtn.onClick.AddListener(OnUpgrade);
    }

    private void Start()
    {
        EnhanceItemContent = MainUI.instance.EnhanceItemContent;
    }

    private void ItemContentDisplay_OnItemContentDisplayChanged(object sender, ItemContentDisplay.ItemContentEvent e)
    {
        upgradableItems = e.iItem as UpgradableItems;
        UpdateVisual();
    }

    // Update is called once per frame
    private void OnUpgrade()
    {
        EnhanceItemContent.SetIItem(upgradableItems);
    }

    private void UpdateVisual()
    {
        gameObject.SetActive(upgradableItems != null);
    }

    private void OnDestroy()
    {
        ItemContentDisplay.OnItemContentDisplayChanged -= ItemContentDisplay_OnItemContentDisplayChanged;
    }
}
