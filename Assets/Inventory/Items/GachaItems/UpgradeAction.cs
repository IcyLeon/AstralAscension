using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeAction : MonoBehaviour
{
    [SerializeField] private ItemContentDisplay ItemContentDisplay;
    private UpgradeItemContent UpgradeItemContent;
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
        UpgradeItemContent = MainUI.instance.UpgradeItemContent;
    }

    private void ItemContentDisplay_OnItemContentDisplayChanged(object sender, ItemContentDisplay.ItemContentEvent e)
    {
        upgradableItems = e.iItem as UpgradableItems;
        UpdateVisual();
    }

    // Update is called once per frame
    private void OnUpgrade()
    {
        UpgradeItemContent.SetIItem(upgradableItems);
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
