using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class ItemContentInformation : MonoBehaviour
{
    private void Awake()
    {
        Init();
    }

    protected abstract void Init();

    public abstract void UpdateItemContentInformation(IItem iItem);
}
