using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class WorldMapUI : MonoBehaviour
{
    private CurrentSelectMapIcon currentSelectMapIcon;
    private Dictionary<IMapIconWidget, MapIcon> IconsDictionary;
    private Canvas canvas;

    [field: SerializeField] public WorldMapBackground WorldMapBackground { get; private set; }

    private WorldMapManager worldMap;

    private DragnDrop dragnDrop;

    private void Awake()
    {
        IconsDictionary = new();

        canvas = GetComponentInParent<Canvas>();

        dragnDrop = GetComponent<DragnDrop>();

        currentSelectMapIcon = GetComponentInChildren<CurrentSelectMapIcon>(true);
    }

    private void Start()
    {
        worldMap = WorldMapManager.instance;
        if (worldMap == null)
        {
            Debug.LogError("World Map Manager is not found!");
            return;
        }

        worldMap.OnMapIconAdd += WorldMap_OnMapIconAdd;
        worldMap.OnMapIconRemove += WorldMap_OnMapIconRemove;
        ResetVisual();
    }

    private void WorldMap_OnMapIconRemove(IMapIconWidget IMapIconWidget)
    {
        if (!IconsDictionary.TryGetValue(IMapIconWidget, out MapIcon mapIcon))
            return;

        IconsDictionary.Remove(IMapIconWidget);

        if (mapIcon == null)
            return;

        mapIcon.OnMapIconClick -= MapIcon_OnMapIconClick;
        Destroy(mapIcon.gameObject);

    }

    private void ResetVisual()
    {
        for (int i = IconsDictionary.Count - 1; i >= 0; i--)
        {
            WorldMap_OnMapIconRemove(IconsDictionary.ElementAt(i).Key);
        }

        foreach(var IMapIconWidget in worldMap.IMapIconWidgetList)
        {
            WorldMap_OnMapIconAdd(IMapIconWidget);
        }
    }

    private void WorldMap_OnMapIconAdd(IMapIconWidget iMapIconWidget)
    {
        if (IconsDictionary.ContainsKey(iMapIconWidget))
            return;

        MapIcon mapIcon = WorldMapBackground.SpawnMapIcon(iMapIconWidget);
        mapIcon.OnMapIconClick += MapIcon_OnMapIconClick;
        IconsDictionary.Add(iMapIconWidget, mapIcon);
    }

    private void MapIcon_OnMapIconClick(object sender, MapIconEvent MapIconEvent)
    {
        currentSelectMapIcon.SetMapIcon(MapIconEvent.MapIcon);
    }

    private void OnEnable()
    {
        dragnDrop.OnDragEvent += DragnDrop_OnDragEvent;
    }

    private void OnDisable()
    {
        dragnDrop.OnDragEvent -= DragnDrop_OnDragEvent;
    }

    private void OnDestroy()
    {
        if (worldMap != null) 
        {
            worldMap.OnMapIconAdd -= WorldMap_OnMapIconAdd;
        }

        dragnDrop.OnDragEvent -= DragnDrop_OnDragEvent;
    }

    private void DragnDrop_OnDragEvent(object sender, DragEvent DE)
    {
        if (DE.eventData.button != PointerEventData.InputButton.Left)
            return;

        RectTransform rt = WorldMapBackground.MapRT;

        rt.anchoredPosition += DE.eventData.delta / canvas.scaleFactor;

        Vector2 clampedanchoredPosition = rt.anchoredPosition;
        clampedanchoredPosition.x = Mathf.Clamp(clampedanchoredPosition.x, -WorldMapBackground.GetMapSize().x / 2f, WorldMapBackground.GetMapSize().x / 2f);
        clampedanchoredPosition.y = Mathf.Clamp(clampedanchoredPosition.y, -WorldMapBackground.GetMapSize().y / 2f, WorldMapBackground.GetMapSize().y / 2f);
        rt.anchoredPosition = clampedanchoredPosition;
    }
}
