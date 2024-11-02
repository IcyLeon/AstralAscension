using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSelectionPanStorage
{
    private CameraSelectionPan currentCameraSelectionPan;
    public CameraPanData cameraPanData { get; }
    public FreeLookSelectionPan freeLookSelectionPan { get; }
    public CloseUpSelectionPan closeUpSelectionPan { get; }
    public CameraSelectionPanStorage(CameraRotationAroundTarget cameraRotationAroundTarget)
    {
        cameraPanData = new CameraPanData(cameraRotationAroundTarget);
        freeLookSelectionPan = new FreeLookSelectionPan(this);
        closeUpSelectionPan = new CloseUpSelectionPan(this);
        ChangeCameraPanType(freeLookSelectionPan);
    }

    public void Update()
    {
        if (currentCameraSelectionPan == null)
            return;

        currentCameraSelectionPan.Update();
    }

    public void OnDrag(Vector2 delta)
    {
        if (currentCameraSelectionPan == null)
            return;

        currentCameraSelectionPan.OnDrag(delta);
    }

    public void OnScroll(Vector2 delta)
    {
        if (currentCameraSelectionPan == null)
            return;

        currentCameraSelectionPan.OnScroll(delta);
    }

    public void ChangeCameraPanType(CameraSelectionPan cameraSelectionPan)
    {
        if (currentCameraSelectionPan != null)
        {
            currentCameraSelectionPan.Exit();
        }

        currentCameraSelectionPan = cameraSelectionPan;

        currentCameraSelectionPan.Enter();
    }
}
