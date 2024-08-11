using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMapIconAction : MapIconAction
{
    public PlayerMapIconAction(MapIcon mapIcon) : base(mapIcon)
    {
        mapIcon.worldMapBackground.OnMapIconAdd += OnMapIconAdd;

        Image mapIconImage = mapIcon.GetComponent<Image>();

        if (mapIconImage == null)
            return;

        mapIconImage.raycastTarget = false;
    }

    private void OnMapIconAdd(MapIcon mapIcon)
    {
        this.mapIcon.transform.SetAsLastSibling();
    }

    public override bool ShowActionOption()
    {
        return false;
    }

    public override string GetActionText()
    {
        return "";
    }
    public override void Action()
    {
    }

    public override void Update()
    {
        base.Update();
        UpdateRotation();
    }


    public void MapCentering()
    {
        Vector2 direction = (mapIcon.worldMapBackground.GetMapSize() * 0.5f) - (mapIcon.RT.anchoredPosition) - (mapIcon.worldMapBackground.MapRT.anchoredPosition + mapIcon.worldMapBackground.OffsetPositionCenter());
        mapIcon.worldMapBackground.MapRT.anchoredPosition += direction;
    }

    private void UpdateRotation()
    {
        if (mapIcon.mapObject == null)
            return;

        Quaternion rotation = Quaternion.Euler(0f, 0f, 360f - mapIcon.mapObject.GetMapIconTransform().eulerAngles.y);
        mapIcon.RT.rotation = rotation;
    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        mapIcon.worldMapBackground.OnMapIconAdd -= OnMapIconAdd;
    }
}
