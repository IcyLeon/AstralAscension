using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FreeLookCameraPanData
{
    [field: SerializeField] public float BaseCameraDistance { get; private set; }
    [field: SerializeField] public float BaseCameraRotationSpeed { get; private set; }
    [field: SerializeField] public float BaseCameraRotationSmoothingSpeed { get; private set; }
    [field: SerializeField] public float BaseCameraZoomSmoothingSpeed { get; private set; }
    [field: SerializeField] public float CameraZoomSpeed { get; private set; }
}
