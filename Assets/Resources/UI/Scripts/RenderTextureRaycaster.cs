using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RenderTextureRaycaster : GraphicRaycaster
{
    // Called by Unity when a Raycaster should raycast because it extends BaseRaycaster.
    public override void Raycast(PointerEventData eventData, List<RaycastResult> resultAppendList)
    {
        Ray ray = eventCamera.ScreenPointToRay(eventData.position); // Mouse
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit))
        {
            OnRaycastHit(hit, resultAppendList);
        }
    }

    private void OnRaycastHit(RaycastHit OriginHit, List<RaycastResult> resultAppendList)
    {
        Vector3 virtualPos = new Vector3(OriginHit.textureCoord.x, OriginHit.textureCoord.y);
        Ray ray = eventCamera.ViewportPointToRay(virtualPos);

        Debug.Log(OriginHit.collider);
    }
}
