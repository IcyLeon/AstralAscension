using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseUpSelectionPan : CameraSelectionPan
{
    public CloseUpSelectionPan(CameraSelectionPanStorage CameraSelectionPanStorage) : base(CameraSelectionPanStorage)
    {
    }

    public override void Enter()
    {
        base.Enter();
        cameraSelectionPanStorage.cameraPanData.targetCameraDistance = cameraSelectionPanStorage.cameraPanData.closeUpCameraPanData.CameraDistance;
        cameraSelectionPanStorage.cameraPanData.cameraZoomSpeed = 0f;
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void OnScroll(Vector2 delta)
    {

    }

    public override void OnDrag(Vector2 delta)
    {

    }
}
