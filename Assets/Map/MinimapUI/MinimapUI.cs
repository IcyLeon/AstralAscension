using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinimapUI : MapUI
{
    private PlayerMapIcon playerMapIcon;

    protected override void OnMapIconAdd(MapIcon mapIcon)
    {
        base.OnMapIconAdd(mapIcon);

        PlayerMapIcon playerIcon = mapIcon as PlayerMapIcon;

        if (playerIcon == null)
        {
            return;
        }

        playerMapIcon = playerIcon;

        worldMapBackground.OnMapIconAdd -= OnMapIconAdd;
    }

    private void Update()
    {
        UpdatePlayerIcon();
    }

    protected void UpdatePlayerIcon()
    {
        if (playerMapIcon == null)
            return;

        Vector2 direction = (playerMapIcon.worldMapBackground.GetMapSize() * 0.5f) - (playerMapIcon.RT.anchoredPosition) - (playerMapIcon.worldMapBackground.MapRT.anchoredPosition + playerMapIcon.worldMapBackground.OffsetPositionCenter());
        playerMapIcon.worldMapBackground.MapRT.anchoredPosition += direction;
    }
}
