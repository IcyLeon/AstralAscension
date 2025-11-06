using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemFamilyTypeSO", menuName = "ScriptableObjects/ItemManager/ItemFamilyTypeSO")]
public class ItemFamilyTypeSO : ScriptableObject
{
    [field: SerializeField] public string ItemFamilyType { get; private set; }
}
