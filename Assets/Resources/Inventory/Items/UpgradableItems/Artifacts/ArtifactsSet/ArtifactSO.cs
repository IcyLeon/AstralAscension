using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactSO", menuName = "ScriptableObjects/ArtifactManager/ArtifactSO")]
public class ArtifactSO : ItemSO
{
    [field: SerializeField, Header("Artifact Piece")] public ArtifactFamilySO ArtifactFamilySO { get; private set; }

    public override Item CreateItem()
    {
        return new Artifact(this);
    }
}
