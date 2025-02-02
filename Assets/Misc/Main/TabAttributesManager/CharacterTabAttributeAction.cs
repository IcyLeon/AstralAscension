using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class CharacterTabAttributeAction : MonoBehaviour
{
    [field: SerializeField] public TabAttributeSO TabAttributeSO { get; private set; }
    [field: SerializeField] public CameraPanVirtualCam CameraPanVirtualCam { get; private set; }

    protected virtual void Awake()
    {
    }

    protected virtual void Start()
    {

    }

    public virtual void SetScreenPanel(CharacterScreenPanel Panel)
    {

    }

    public virtual void OnEnter()
    {
    }

    public virtual void OnExit()
    {
    }
}
