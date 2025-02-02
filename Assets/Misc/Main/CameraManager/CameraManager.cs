using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraManager
{
    private static CameraManager cameraManagerInstance;
    private Dictionary<Scene, MainCamera> mainCameras;
    public static CameraManager instance
    {
        get
        {
            return GetInstance();
        }
    }

    private CameraManager()
    {
        mainCameras = new();
    }

    private static CameraManager GetInstance()
    {
        if (cameraManagerInstance == null)
        {
            cameraManagerInstance = new CameraManager();
        }

        return cameraManagerInstance;
    }

    public void Register(MainCamera newMainCamera)
    {
        Scene scene = newMainCamera.Camera.scene;

        if (GetMainCamera(scene) != null)
            return;

        newMainCamera.OnCameraDestroy += NewMainCamera_OnCameraDestroy;
        mainCameras.Add(scene, newMainCamera);
    }

    private void NewMainCamera_OnCameraDestroy(MainCamera MainCamera)
    {
        mainCameras.Remove(MainCamera.Camera.scene);
    }

    public MainCamera GetMainCamera(Scene Scene)
    {
        if (mainCameras.TryGetValue(Scene, out MainCamera MainCamera))
            return MainCamera;

        return null;
    }
}
