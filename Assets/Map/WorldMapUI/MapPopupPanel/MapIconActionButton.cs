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

    private Player player;

    private MapPopupPanel MapPopupPanel;
    private MapIcon mapIcon;

    private void Awake()
    {
        player = GetComponentInParent<Player>();

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

        gameObject.SetActive(mapIcon.mapObject != null);

        if (!gameObject.activeSelf)
            return;

        ButtonText.text = mapIcon.mapObject.GetActionText();
    }

    private void OnButtonClick()
    {
        if (mapIcon == null || mapIcon.mapObject == null)
            return;

        mapIcon.mapObject.Action(player);
    }

    private void OnDestroy()
    {
        MapPopupPanel.OnMapIconChanged -= CurrentSelectMapIcon_OnMapIconChanged;
    }
}
