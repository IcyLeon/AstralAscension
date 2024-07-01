using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ItemRaritySO", menuName = "ScriptableObjects/ItemManager/ItemRaritySO")]
public class ItemRaritySO : ScriptableObject
{
    [field: SerializeField] public Sprite ItemQualityBackground { get; private set; }
    [field: SerializeField] public Sprite ItemCardBackground { get; private set; }
    [field: SerializeField] public Rarity Rarity { get; private set; }
}
