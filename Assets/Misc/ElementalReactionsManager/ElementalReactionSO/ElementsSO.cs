using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementSO", menuName = "ScriptableObjects/ElementManager/ElementSO")]
public class ElementsSO : ScriptableObject
{
    [field: SerializeField] public Sprite ElementSprite { get; private set; }
    [field: SerializeField] public Color ColorText { get; private set; }

    public Elements CreateElements(IDamageable target)
    {
        return new Elements(this, target);
    }
}
