using GD.MinMaxSlider;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class CameraDefaultData
{
    [field: SerializeField, MinMaxSlider(0, 10)] public Vector2 CameraDistanceRange { get; private set; }
    [field: SerializeField] public float CameraZoomSmoothingSpeed { get; private set; }
}
