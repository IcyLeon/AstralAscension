using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMapIconAction : MapIconAction
{
    public PlayerMapIconAction(MapIcon mapIcon) : base(mapIcon)
    {
        if (mapIcon == null)
            return;

        mapIcon.GetComponent<Image>().raycastTarget = false;
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
        if (mapIcon.iMapIconWidget == null)
            return;

        Quaternion rotation = Quaternion.Euler(0f, 0f, 360f - mapIcon.iMapIconWidget.GetMapIconTransform().eulerAngles.y);
        mapIcon.RT.rotation = rotation;
    }


}
