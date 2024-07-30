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

    public override void Update()
    {
        base.Update();
        //MapCentering();
        UpdateRotation();
    }

    private void MapCentering()
    {
        Vector2 direction = (mapIcon.RT.anchoredPosition) - (mapIcon.worldMapBackground.GetMapSize() * 0.5f) + mapIcon.worldMapBackground.MapRT.anchoredPosition;
        mapIcon.worldMapBackground.MapRT.anchoredPosition -= direction;
    }

    private void UpdateRotation()
    {
        if (mapIcon.iMapIconWidget == null)
            return;

        Quaternion rotation = Quaternion.Euler(0f, 0f, 360f - mapIcon.iMapIconWidget.GetMapIconTransform().eulerAngles.y);
        mapIcon.RT.rotation = rotation;
    }


}
