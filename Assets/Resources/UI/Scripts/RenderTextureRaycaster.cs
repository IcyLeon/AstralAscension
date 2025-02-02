using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RenderTextureRaycaster : MonoBehaviour, IPointerClickHandler
{
    private GraphicRaycaster GraphicRaycaster;

    private void Awake()
    {
        GraphicRaycaster = GetComponentInParent<GraphicRaycaster>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log(GraphicRaycaster.eventCamera);
    }
}
