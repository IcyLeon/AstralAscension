using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TabAttributeSO", menuName = "ScriptableObjects/TabManager/TabAttributeSO")]

public class TabAttributeSO : ScriptableObject
{
    [field: SerializeField] public string TabName { get; private set; }
}
