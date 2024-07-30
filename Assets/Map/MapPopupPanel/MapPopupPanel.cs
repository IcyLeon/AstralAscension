using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapPopupPanel : MonoBehaviour
{
    [Header("Title")]
    [SerializeField] private TextMeshProUGUI IconTitle;
    [SerializeField] private Image IconImage;
    private MapIcon mapIcon;

    public void SetMapIcon(MapIcon MapIcon)
    {
        mapIcon = MapIcon;
        gameObject.SetActive(mapIcon != null);
        UpdateVisual();
    }


    private void UpdateVisual()
    {
        if (mapIcon == null || mapIcon.iMapIconWidget == null || mapIcon.iMapIconWidget.GetMapIconTypeSO() == null)
            return;

        IconTitle.text = mapIcon.iMapIconWidget.GetMapIconTypeSO().IconName;
        IconImage.sprite = mapIcon.iMapIconWidget.GetMapIconTypeSO().IconSprite;
    }
}
