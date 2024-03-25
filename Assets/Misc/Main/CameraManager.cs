using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public Camera CameraMain { get; private set; }
    [field: SerializeField] public Player Player { get; private set; }

    private void Awake()
    {
        transform.SetParent(null);
    }
    // Start is called before the first frame update
    void Start()
    {
        CameraMain = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
