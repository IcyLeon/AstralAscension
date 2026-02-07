using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class TabAttributesPanel : MonoBehaviour
{

    protected virtual void Awake()
    {

    }

    // Start is called before the first frame update
    protected virtual void Start()
    {

    }

    protected virtual void OnEnable()
    {
        TabAttributesMiscEvent.Reset();
    }

    protected virtual void OnDisable()
    {
        TabAttributesMiscEvent.Reset();
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        
    }
}
