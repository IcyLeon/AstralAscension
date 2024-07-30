using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class CurrentSelectMapIcon : MonoBehaviour
{
    [Header("Title")]
    [SerializeField] private TextMeshProUGUI IconTitle;
    [SerializeField] private Image IconImage;
    private MapIcon mapIcon;

    public void SetMapIcon(MapIcon MapIcon)
    {
        mapIcon = MapIcon;

        gameObject.SetActive(mapIcon != null);

        if (mapIcon != null)
        {
            UpdateVisual();
        }
    }

    private void UpdateVisual()
    {
        if (mapIcon.iMapIconWidget == null || mapIcon.iMapIconWidget.GetMapIconTypeSO() == null)
            return;

        IconTitle.text = mapIcon.iMapIconWidget.GetMapIconTypeSO().IconName;
        IconImage.sprite = mapIcon.iMapIconWidget.GetMapIconTypeSO().IconSprite;
    }

    private void OnDestroy()
    {
    }
}
