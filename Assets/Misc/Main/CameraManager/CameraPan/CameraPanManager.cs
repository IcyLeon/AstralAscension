using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPanManager : MonoBehaviour
{
    [field: SerializeField] public CameraPanSelectionDataSO CameraPanSelectionDataSO { get; private set; }
    public FreeLookCameraPanVirtualCamera freeLookCameraPanVirtualCamera { get; private set; }
    public CharacterProfileCameraPanVirtualCamera characterProfileCameraPanVirtualCamera { get; private set; }
    private CameraPanVirtualCam currentPanVirtualCam;

    private void Awake()
    {
        freeLookCameraPanVirtualCamera = GetComponentInChildren<FreeLookCameraPanVirtualCamera>();
        characterProfileCameraPanVirtualCamera = GetComponentInChildren<CharacterProfileCameraPanVirtualCamera>();

        DisableAllVirtualCams();
    }

    private void DisableAllVirtualCams()
    {
        CameraPanVirtualCam[] CameraPanVirtualCamList = GetComponentsInChildren<CameraPanVirtualCam>(true);

        foreach (var Cam in CameraPanVirtualCamList)
        {
            Cam.gameObject.SetActive(false);
        }
    }

    private void Start()
    {
        ChangeCamera(freeLookCameraPanVirtualCamera);
    }

    public void OnDrag(Vector2 delta)
    {
        if (currentPanVirtualCam == null)
            return;

        currentPanVirtualCam.OnDrag(delta);
    }

    public void OnScroll(float delta)
    {
        if (currentPanVirtualCam == null)
            return;

        currentPanVirtualCam.OnScroll(delta);
    }

    public void ChangeCamera(CameraPanVirtualCam CameraPanVirtualCam)
    {
        if (currentPanVirtualCam != null)
        {
            currentPanVirtualCam.gameObject.SetActive(false);
        }

        currentPanVirtualCam = CameraPanVirtualCam;

        currentPanVirtualCam.gameObject.SetActive(true);
    }
}
