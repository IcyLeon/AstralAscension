using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera MainCamera;

    // Start is called before the first frame update
    void Start()
    {
        MainCamera = Camera.main;
        SetGlobalScale(transform.localScale);

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
