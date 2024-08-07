using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private PlayerMapIconAction playerMapIconAction;
    private WorldMapBackground WorldMapBackground;

    private void Awake()
    {
        WorldMapBackground = GetComponentInChildren<WorldMapBackground>();
        WorldMapBackground.OnMapIconAdd += WorldMapBackground_OnMapIconAdd;
    }

    private void WorldMapBackground_OnMapIconAdd(MapIcon mapIcon)
    {
        PlayerMapIconAction playerMapIconAction = mapIcon.mapIconAction as PlayerMapIconAction;

        if (playerMapIconAction == null)
        {
            return;
        }

        this.playerMapIconAction = playerMapIconAction;

        WorldMapBackground.OnMapIconAdd -= WorldMapBackground_OnMapIconAdd;
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

    private void OnDestroy()
    {
        WorldMapBackground.OnMapIconAdd -= WorldMapBackground_OnMapIconAdd;
    }
}
