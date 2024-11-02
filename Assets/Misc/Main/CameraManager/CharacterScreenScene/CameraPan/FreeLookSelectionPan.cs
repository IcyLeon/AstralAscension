using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreeLookSelectionPan : CameraSelectionPan
{
    public FreeLookSelectionPan(CameraSelectionPanStorage CameraSelectionPanStorage) : base(CameraSelectionPanStorage)
    {
    }

    public override void Enter()
    {
        base.Enter();
        InitBaseData();
        cameraSelectionPanStorage.cameraPanData.cameraZoomSpeed = cameraSelectionPanStorage.cameraPanData.freeLookCameraPanData.CameraZoomSpeed;
    }

    public override void Exit()
    {
        base.Exit();
        cameraSelectionPanStorage.cameraPanData.ResetRotation();
    }


    public override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            cameraSelectionPanStorage.ChangeCameraPanType(cameraSelectionPanStorage.closeUpSelectionPan);
            return;
        }
    }

    public override void OnScroll(Vector2 delta)
    {
        base.OnScroll(delta); 


    }
}
