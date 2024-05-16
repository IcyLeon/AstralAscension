using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ElementSO", menuName = "ScriptableObjects/ElementManager/ElementSO")]
public class ElementsSO : ElementsInfoSO
{
    [field: SerializeField] public Sprite ElementSprite { get; private set; }

    [ColorUsage(true, true)]
    [SerializeField] private Color GlowIconColor;

    public Color GetGlowIconColor()
    {
        return GlowIconColor;
    }

    public Elements CreateElements(IDamageable target)
    {
        return new Elements(this, target);
    }
}
