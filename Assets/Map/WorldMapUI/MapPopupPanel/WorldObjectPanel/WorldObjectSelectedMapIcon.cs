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
        return mapIcon.mapIconAction is InteractiveMapIconAction;
    }

    private void UpdateDescription()
    {
        IconTitle.text = mapIcon.mapObject.GetMapIconTypeSO().IconName;
        IconImage.sprite = mapIcon.mapObject.GetMapIconTypeSO().IconSprite;
        IconDescription.text = mapIcon.mapObject.GetMapIconTypeSO().IconDescription;
    }
}
