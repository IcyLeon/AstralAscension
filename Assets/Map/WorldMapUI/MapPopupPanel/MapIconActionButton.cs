using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapIconActionButton : MonoBehaviour
{
    [SerializeField] private Button ActionButton;
    [SerializeField] private TextMeshProUGUI ButtonText;
    private MapPopupPanel MapPopupPanel;
    private MapIcon mapIcon;

    private void Awake()
    {
        ActionButton.onClick.AddListener(OnButtonClick);

        MapPopupPanel = GetComponentInParent<MapPopupPanel>(true);
        MapPopupPanel.OnMapIconChanged += CurrentSelectMapIcon_OnMapIconChanged;
        UpdateVisual();
    }

    private void CurrentSelectMapIcon_OnMapIconChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        mapIcon = MapPopupPanel.mapIcon;

        gameObject.SetActive(mapIcon.mapIconAction != null && mapIcon.mapIconAction.ShowActionOption());

        if (!gameObject.activeSelf)
            return;

        ButtonText.text = mapIcon.mapIconAction.GetActionText();
    }

    private void OnButtonClick()
    {
        if (mapIcon == null || mapIcon.mapIconAction == null)
            return;

        mapIcon.mapIconAction.Action();
    }

    private void OnDestroy()
    {
        MapPopupPanel.OnMapIconChanged -= CurrentSelectMapIcon_OnMapIconChanged;
    }
}
