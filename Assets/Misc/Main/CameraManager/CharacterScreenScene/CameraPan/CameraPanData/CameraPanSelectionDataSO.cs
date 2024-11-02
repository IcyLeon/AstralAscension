using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CameraPanSelectionDataSO", menuName = "ScriptableObjects/CameraManager/CameraViewPanManager/CameraPanSelectionDataSO")]
public class CameraPanSelectionDataSO : ScriptableObject
{
    [field: SerializeField] public FreeLookCameraPanData FreeLookCameraPanData { get; private set; }
    [field: SerializeField] public CloseUpCameraPanData CloseUpCameraPanData { get; private set; }
}
