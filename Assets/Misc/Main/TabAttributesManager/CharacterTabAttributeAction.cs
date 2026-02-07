using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static CharacterTabAttributeActionManager;

[DisallowMultipleComponent]
public abstract class CharacterTabAttributeAction : MonoBehaviour
{
    [field: SerializeField] public TAB_ATTRIBUTE TabAttribute { get; private set; }
    [SerializeField] private CameraPanVirtualCam CameraPanVirtualCam;
    private CharacterTabAttributeActionManager actionManager;

    protected virtual void Awake()
    {
        actionManager = GetComponentInParent<CharacterTabAttributeActionManager>();
    }

    protected virtual void Start()
    {

    }

    public virtual void SetScreenPanel(CharacterScreenPanel Panel)
    {
    }

    public virtual void OnEnter()
    {
        ResetCamera();
    }

    protected void ResetCamera()
    {
        actionManager.cameraPanManager.ChangeCamera(CameraPanVirtualCam);
    }

    public virtual void OnExit()
    {
    }
}
