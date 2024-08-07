using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MapPopupPanel : MonoBehaviour
{
    public event EventHandler OnMapIconChanged;
    private CurrentSelectMapIcon[] MapIconPanels;

    public MapIcon mapIcon { get; private set; }

    private void Awake()
    {
        MapIconPanels = GetComponentsInChildren<CurrentSelectMapIcon>(true);

        foreach (var MapIconPanel in MapIconPanels)
        {
            MapIconPanel.Init();
        }
    }

    public void SetMapIcon(MapIcon MapIcon)
    {
        mapIcon = MapIcon;

        gameObject.SetActive(mapIcon != null);

        OnMapIconChanged?.Invoke(this, EventArgs.Empty);
    }
}
