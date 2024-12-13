using GD.MinMaxSlider;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraSO", menuName = "ScriptableObjects/CameraManager/CameraSO")]
public class CameraSO : ScriptableObject
{
    [field: SerializeField] public CameraDefaultData CameraDefaultData { get; private set; }
    [field: SerializeField] public CameraAimData CameraAimData { get; private set; }
}
