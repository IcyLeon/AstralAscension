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

        Canvas canvas = GetComponent<Canvas>();
        if (canvas != null)
            canvas.worldCamera = MainCamera;
    }

    // Update is called once per frame
    void Update()
    {
        if (MainCamera != null)
            transform.rotation = MainCamera.transform.rotation;
    }
}
