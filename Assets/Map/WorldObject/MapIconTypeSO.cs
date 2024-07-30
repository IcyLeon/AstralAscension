using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "MapIconTypeSO", menuName = "ScriptableObjects/MapManager/MapIconTypeSO")]
public class MapIconTypeSO : ScriptableObject
{
    [field: SerializeField] public string IconName { get; private set; }
    [field: SerializeField] public Sprite IconSprite { get; private set; }
    [field: SerializeField, TextArea] public string IconDescription { get; private set; }


}
