using GD.MinMaxSlider;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraPanSelectionDataSO", menuName = "ScriptableObjects/CameraManager/CameraViewPanManager/CameraPanSelectionDataSO")]
public class CameraPanSelectionDataSO : ScriptableObject
{
    [field: SerializeField] public float CameraDistance { get; private set; }
    [field: SerializeField, MinMaxSlider(0, 10)] public Vector2 CameraDistanceRange { get; private set; }
    [field: SerializeField] public float CameraRotationSpeed { get; private set; }
    [field: SerializeField] public float CameraRotationSmoothingSpeed { get; private set; }
    [field: SerializeField] public float CameraZoomSmoothingSpeed { get; private set; }
    [field: SerializeField] public float CameraZoomSpeed { get; private set; }
}
