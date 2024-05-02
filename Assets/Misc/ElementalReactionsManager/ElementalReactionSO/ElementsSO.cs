using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementSO", menuName = "ScriptableObjects/ElementManager/ElementSO")]
public class ElementsSO : ScriptableObject
{
    [field: SerializeField] public ElementsEnums ElementsEnums { get; private set; }
    [field: SerializeField] public Sprite ElementSprite { get; private set; }
    [field: SerializeField] public Color32 Color { get; private set; }

    public Elements CreateElements()
    {
        return new Elements(this);
    }
}
