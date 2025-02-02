using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Billboard : MonoBehaviour
{
    //[SerializeField] private string CameraTag = "MainCamera";
    private Camera MainCamera;

    // Start is called before the first frame update
    private void Start()
    {
        SetGlobalScale(transform.localScale);
        CameraInit();
    }
    

    private void CameraInit()
    {
        MainCamera = GetMainCamera();

        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
            canvas.worldCamera = MainCamera;
    }

    private Camera GetMainCamera()
    {
        Camera DefaultMain = Camera.main;

        CameraManager cameraManager = CameraManager.instance;

        if (cameraManager == null)
            return DefaultMain;

        MainCamera mainCamera = cameraManager.GetMainCamera(gameObject.scene);

        if (mainCamera != null)
        {
            return mainCamera.Camera;
        }

        return DefaultMain;
    }

    private void SetGlobalScale(Vector3 globalScale)
    {
        if (transform.parent == null)
        {
            transform.localScale = globalScale;
        }
        else
        {
            Vector3 parentGlobalScale = transform.parent.localScale;
            transform.localScale = new Vector3(
                globalScale.x / parentGlobalScale.x,
                globalScale.y / parentGlobalScale.y,
                globalScale.z / parentGlobalScale.z
            );
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (MainCamera != null)
            transform.rotation = MainCamera.transform.rotation;
    }
}
