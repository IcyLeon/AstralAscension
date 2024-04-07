using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetOrb : MonoBehaviour
{
    [SerializeField] AudioSource AimAudioSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {

        AimAudioSource.Play();
    }

    private void OnDisable()
    {
        AimAudioSource.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
