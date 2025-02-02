using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlacedMarkerContent : MarkerPinContent
{
    [SerializeField] private Button ConfirmedButton;

    // Start is called before the first frame update
    protected override void Awake()
    {
        base.Awake();
        ConfirmedButton.onClick.AddListener(OnConfirmedClick);
    }

    private void OnConfirmedClick()
    {
        if (playerMarkerIconData == null)
            return;

        playerMarkerIconData.ConfirmPlacement();

        markerSelectedMapIcon.mapPopupPanel.TogglePanel(false);
    }

    protected override bool IsContentVisible()
    {
        return !playerMarkerIconData.IsConfirmedPlaced();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        ConfirmedButton.onClick.RemoveAllListeners();
    }
}
