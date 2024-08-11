using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapUI : MapUI
{
    private PlayerMapIconAction playerMapIconAction;

    protected override void OnMapIconAdd(MapIcon mapIcon)
    {
        base.OnMapIconAdd(mapIcon);

        PlayerMapIconAction playerMapIconAction = mapIcon.mapIconAction as PlayerMapIconAction;

        if (playerMapIconAction == null)
        {
            return;
        }

        this.playerMapIconAction = playerMapIconAction;

        worldMapBackground.OnMapIconAdd -= OnMapIconAdd;
    }

    private void Update()
    {
        UpdatePlayerIcon();
    }

    private void UpdatePlayerIcon()
    {
        if (playerMapIconAction == null)
            return;

        playerMapIconAction.MapCentering();
    }
}
