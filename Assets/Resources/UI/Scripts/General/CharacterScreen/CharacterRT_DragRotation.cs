using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[DisallowMultipleComponent]
public class CharacterRT_DragRotation : MonoBehaviour, IScrollHandler, IDragHandler
{
    private CharacterSelection characterSelection;

    private void Awake()
    {
    }

    private void Start()
    {
        characterSelection = CharacterSelection.instance;
    }

    public void OnScroll(PointerEventData eventData)
    {
        if (characterSelection == null)
            return;

        CameraPanManager c = characterSelection.cameraPanManager;
        c.OnScroll(eventData.scrollDelta.y);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (characterSelection == null)
            return;

        CameraPanManager c = characterSelection.cameraPanManager;
        c.OnDrag(eventData.delta);
    }
}
