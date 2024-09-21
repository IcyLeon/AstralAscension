using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerMapIcon : MapIcon
{
    [SerializeField] private RectTransform FOV_RT;
    private Camera playerCamera;

    public override void SetMapObject(MapObject MapObject, WorldMapBackground WorldMapBackground)
    {
        base.SetMapObject(MapObject, WorldMapBackground);
        worldMapBackground.OnMapIconAdd += OnMapIconAdd;
        worldMapBackground.OnMapIconAdd += OnPlayerMapIconAdd;
    }

    private void OnPlayerMapIconAdd(MapIcon mapIcon)
    {
        PlayerIcon playerIcon = mapIcon.mapObject as PlayerIcon;

        if (playerIcon == null)
        {
            return;
        }

        Player player = playerIcon.GetComponent<Player>();

        if (player == null)
            return;

        playerCamera = player.CameraManager.CameraMain;

        worldMapBackground.OnMapIconAdd -= OnPlayerMapIconAdd;
    }

    private void OnMapIconAdd(MapIcon mapIcon)
    {
        transform.SetAsLastSibling();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        worldMapBackground.OnMapIconAdd -= OnMapIconAdd;
        worldMapBackground.OnMapIconAdd -= OnPlayerMapIconAdd;
    }

    protected override void Update()
    {
        base.Update();
        UpdateRotation();
        UpdateFOVRotation();
    }

    private void UpdateFOVRotation()
    {
        if (playerCamera == null)
            return;

        Quaternion rotation = Quaternion.Euler(0f, 0f, 360 - playerCamera.transform.eulerAngles.y);
        FOV_RT.rotation = rotation;
    }

    private void UpdateRotation()
    {
        if (mapObject == null)
            return;

        Quaternion rotation = Quaternion.Euler(0f, 0f, 360f - mapObject.GetMapIconTransform().eulerAngles.y);
        RT.rotation = rotation;
    }
}
