using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRotationAroundTarget : MonoBehaviour
{
    [field: SerializeField] public CinemachineVirtualCamera VirtualCamera { get; private set; }
    [field: SerializeField] public CameraPanSelectionDataSO CameraPanSelectionDataSO { get; private set; }
}
