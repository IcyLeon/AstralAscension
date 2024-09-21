using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class WorldObjectSelectedMapIcon : CurrentSelectMapIcon
{
    [Header("Icon Base Information")]
    [SerializeField] private TextMeshProUGUI IconTitle;
    [SerializeField] private Image IconImage;
    [SerializeField] private TextMeshProUGUI IconDescription;

    protected override void UpdateInformation()
    {
        UpdateDescription();
    }

    protected override bool IsVisible()
    {
        return mapIcon.mapObject is InteractiveMapObject;
    }

    private void UpdateDescription()
    {
        if (mapIcon.mapObject == null)
            return;

        IconTitle.text = mapIcon.mapObject.mapIconData.mapIconName;
        IconImage.sprite = mapIcon.mapObject.mapIconData.mapIconSprite;
        IconDescription.text = mapIcon.mapObject.mapIconData.mapIconDescription;
    }
}
