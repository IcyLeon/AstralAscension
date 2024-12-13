using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraCaptureScene : MonoBehaviour
{
    private Camera Camera;

    // Start is called before the first frame update
    void Start()
    {
        Camera = GetComponent<Camera>();
        Camera.scene = SceneManager.GetActiveScene();
    }
}
