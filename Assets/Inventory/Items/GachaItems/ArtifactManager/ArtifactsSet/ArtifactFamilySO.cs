using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "ArtifactManager", menuName = "ScriptableObjects/ArtifactManager/ArtifactFamilySO")]
public class ArtifactFamilySO : ScriptableObject
{
    [Serializable]
    public class BuffPieceInfo
    {
        [SerializeField] private GameObject BuffFactoryPrefab;
        [field: SerializeField, Range(0, 4)] public int NoOfPiece { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }

        public ArtifactEffectFactory GetBuffEffect()
        {
            return BuffFactoryPrefab.GetComponent<ArtifactEffectFactory>();
        }
    }

    [field: SerializeField] public string ArtifactSetName { get; private set; }
    [field: SerializeField] public BuffPieceInfo TwoPieceBuff { get; private set; }
    [field: SerializeField] public BuffPieceInfo FourPieceBuff { get; private set; }

    public ArtifactEffectFactoryManager CreateArtifactEffectFactoryManager()
    {
        return new ArtifactEffectFactoryManager(this);
    }


}