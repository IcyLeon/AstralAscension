using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapIconPrefabManagerSO", menuName = "ScriptableObjects/MapManager/MapIconPrefabManagerSO")]
public class MapIconPrefabManagerSO : ScriptableObject
{
    [SerializeField] private GameObject MapIconPrefab;
    [SerializeField] private GameObject PlayerMapIconPrefab;

    public GameObject GetMapIconPrefab(MapObject mapObject)
    {
        if (mapObject is PlayerIcon)
        {
            return PlayerMapIconPrefab;
        }

        return MapIconPrefab;
    }
}
