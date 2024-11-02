using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AssetManager : MonoBehaviour
{
    [field: SerializeField] public ItemManagerSO ItemAssetManagerSO { get; private set; }

    public static AssetManager instance { get; private set; }

    private void Awake()
    {
        instance = this;
    }
}
