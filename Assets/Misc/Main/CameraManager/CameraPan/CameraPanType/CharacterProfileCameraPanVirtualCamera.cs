using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterProfileCameraPanVirtualCamera : CameraPanVirtualCam
{
    protected override void OnEnable()
    {
        base.OnEnable();
    }

    protected override void Update()
    {
        base.Update();

        if (Input.GetKeyDown(KeyCode.Return))
        {
            cameraPanManager.ChangeCamera(cameraPanManager.freeLookCameraPanVirtualCamera);
            return;
        }
    }

    public override void OnScroll(float delta)
    {
    }

    public override void OnDrag(Vector2 delta)
    {
        Vector2 yawRotation = new Vector2(delta.x, 0f);
        base.OnDrag(yawRotation);
    }
}
