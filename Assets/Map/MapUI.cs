using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class MapUI : MonoBehaviour
{
    public WorldMapBackground worldMapBackground { get; protected set; }
    public Player player { get; private set; }

    private void Awake()
    {
        Init();
    }

    public void SetPlayer(Player Player)
    {
        player = Player;
    }

    public virtual void Init()
    {
        if (worldMapBackground != null)
            return;

        worldMapBackground = GetComponentInChildren<WorldMapBackground>();
        worldMapBackground.OnMapIconAdd += OnMapIconAdd;
        worldMapBackground.OnMapIconRemove += OnMapIconRemove;
    }

    protected virtual void OnMapIconAdd(MapIcon mapIcon)
    {

    }
    protected virtual void OnMapIconRemove(MapIcon mapIcon)
    {

    }

    protected virtual void OnDestroy()
    {
        if (worldMapBackground == null)
            return;

        worldMapBackground.OnMapIconAdd -= OnMapIconAdd;
        worldMapBackground.OnMapIconRemove -= OnMapIconRemove;
    }
}
