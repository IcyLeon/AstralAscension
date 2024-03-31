using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraSO", menuName = "ScriptableObjects/CameraManager/CameraSO")]
public class CameraSO : ScriptableObject
{
    [field: SerializeField] public CameraAimData CameraAimData { get; private set; }
    [field: SerializeField] public float minZoom { get; private set; }
    [field: SerializeField] public float maxZoom { get; private set; }

    [field: SerializeField] public float zoomSoothing { get; private set; }
}
