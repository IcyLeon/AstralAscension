using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NavigationMarkerContent : MarkerPinContent
{
    [SerializeField] private Button DeleteButton;
    [SerializeField] private Button NavigationButton;

    protected override void Awake()
    {
        base.Awake();

        DeleteButton.onClick.AddListener(OnDeleteClick);
        NavigationButton.onClick.AddListener(OnNavgiationClick);
    }

    private void OnDeleteClick()
    {
        if (playerMarkerIconData == null)
            return;

        playerMarkerIconData.mapObject.DestroyMapObject();

        markerSelectedMapIcon.mapPopupPanel.TogglePanel(false);
    }

    private void OnNavgiationClick()
    {

    }

    protected override bool IsContentVisible()
    {
        return playerMarkerIconData.IsConfirmedPlaced();
    }
}
