using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class RT_DragRotation : MonoBehaviour, IScrollHandler, IDragHandler
{
    private CharacterSelection characterSelection;

    private void Awake()
    {
    }

    private void Start()
    {
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (CharacterSelection.instance == null)
            return;

        CameraPanManager c = CharacterSelection.instance.cameraPanManager;
        c.OnScroll(eventData.scrollDelta.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (CharacterSelection.instance == null)
            return;

        CameraPanManager c = CharacterSelection.instance.cameraPanManager;
        c.OnDrag(eventData.delta);
    }
}
