using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class UpgradeAction : MonoBehaviour
{
    private ItemContentDisplay ItemContentDisplay;
    private UpgradeItemContent UpgradeItemContent;
    private IEXP iEXPEntity;
    private Button UpgradeBtn;
    private MainUI mainUI;

    private void Awake()
    {
        mainUI = GetComponentInParent<MainUI>();

        ItemContentDisplay = GetComponentInParent<ItemContentDisplay>();
        ItemContentDisplay.OnItemContentDisplayChanged += ItemContentDisplay_OnItemContentDisplayChanged;

        UpgradeBtn = GetComponent<Button>();
        UpgradeBtn.onClick.AddListener(OnUpgrade);

        UpdateVisuals();
    }

    private void Start()
    {
        UpgradeItemContent = mainUI.UpgradeItemContent;
    }

    private void ItemContentDisplay_OnItemContentDisplayChanged()
    {
        iEXPEntity = ItemContentDisplay.iItem as IEXP;
        UpdateVisuals();
    }

    // Update is called once per frame
    private void OnUpgrade()
    {
        UpgradeItemContent.SetIItem(iEXPEntity);
    }

    private void UpdateVisuals()
    {
        gameObject.SetActive(iEXPEntity != null);
    }

    private void OnDestroy()
    {
        ItemContentDisplay.OnItemContentDisplayChanged -= ItemContentDisplay_OnItemContentDisplayChanged;
        UpgradeBtn.onClick.RemoveAllListeners();
    }
}
