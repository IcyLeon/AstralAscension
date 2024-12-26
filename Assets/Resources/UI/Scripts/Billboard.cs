using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    [SerializeField] private string CameraTag = "MainCamera";
    private Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        SetGlobalScale(transform.localScale);
        CameraInit();
    }
    
    private void CameraInit()
    {
        GameObject cameraObj = GameObject.FindGameObjectWithTag(CameraTag);
        MainCamera = cameraObj.GetComponent<Camera>();

        if (MainCamera == null)
        {

            Debug.Log("CameraTag does not have the component of Camera");
            return;
        }

        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
            canvas.worldCamera = MainCamera;
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
