using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MapIconAction
{
    public event EventHandler OnMapIconChanged;

    private Sprite iconSprite;
    protected MapIcon mapIcon { get; private set; }

    public MapIconAction(MapIcon mapIcon)
    {
        this.mapIcon = mapIcon;
    }

    public abstract bool ShowActionOption();
    public abstract string GetActionText();

    public abstract void Action();

    public Sprite GetImageSprite()
    {
        return iconSprite;
    }

    public void SetIconSprite(Sprite sprite)
    {
        iconSprite = sprite;
        OnMapIconChanged?.Invoke(this, EventArgs.Empty);
    }

    public virtual void Update()
    {

    }

    public virtual void OnDestroy()
    {
    }
}
