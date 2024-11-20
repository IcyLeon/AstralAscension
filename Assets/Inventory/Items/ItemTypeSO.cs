using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemTypeSO", menuName = "ScriptableObjects/ItemManager/ItemTypeSO")]
public class ItemTypeSO : ScriptableObject
{
    [field: SerializeField] public string ItemType { get; private set; }
    [field: SerializeField] public ItemFamilyTypeSO ItemFamilyTypeSO { get; private set; }
}
