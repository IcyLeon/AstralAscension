using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ClickUI : MonoBehaviour, IPointerClickHandler
{
    private GraphicRaycaster graphicRaycaster;
    [SerializeField] private GameObject ParentSource;
    [SerializeField] GameObject[] ToggleInvisiblePanelInAscendingOrder;

    private void Awake()
    {
        graphicRaycaster = GetComponentInParent<GraphicRaycaster>();
    }
    private Transform GetTransformClick(PointerEventData eventData)
    {
        List<RaycastResult> hitResults = new List<RaycastResult>();
        graphicRaycaster.Raycast(eventData, hitResults);

        if (hitResults.Count > 0)
        {
            return hitResults[0].gameObject.transform;
        }

        return null;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (eventData.button != PointerEventData.InputButton.Left)
            return;

        Transform currentTransform = GetTransformClick(eventData);

        GameObject go = GetVisibleGO();

        if (go == null)
            return;

        go.SetActive(IsInParentSourceObject(currentTransform));
    }

    private GameObject GetVisibleGO()
    {
        foreach(GameObject go in ToggleInvisiblePanelInAscendingOrder)
        {
            if (go.activeSelf)
                return go;
        }
        return null;
    }

    private bool IsInParentSourceObject(Transform currentObjectClick)
    {
        Transform current = currentObjectClick;

        do
        {
            if (current.gameObject == ParentSource)
            {
                return true;
            }
            current = current.parent;

        } while (current != null && ParentSource != null);

        return false;
    }
}
