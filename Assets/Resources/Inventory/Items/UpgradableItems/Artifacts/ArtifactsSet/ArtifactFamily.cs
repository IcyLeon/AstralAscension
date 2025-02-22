using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactFamily
{
    public List<Artifact> _artifacts { get; }
    private ArtifactEffectFactoryManager ArtifactEffectFactoryManager;
    public ArtifactFamilySO artifactFamilySO { get; private set; }
    private ArtifactBuffInformation current;

    public delegate void OnArtifactBuffChanged(ArtifactBuffInformation ArtifactBuffInformation);
    public event OnArtifactBuffChanged OnArtifactBuffAdd;
    public event OnArtifactBuffChanged OnArtifactBuffRemove;

    public event Action<ArtifactFamily> OnFamilyRemove;
    private int buffIndex;

    public ArtifactFamily(ArtifactFamilySO ArtifactFamilySO)
    {
        _artifacts = new();
        buffIndex = -1;
        artifactFamilySO = ArtifactFamilySO;
        ArtifactEffectFactoryManager = new ArtifactEffectFactoryManager(artifactFamilySO);
    }

    private int GetTotalAmount()
    {
        return _artifacts.Count;
    }

    public void Add(Artifact artifact)
    {
        _artifacts.Add(artifact);

        ArtifactBuffInformation nextBuff = ArtifactEffectFactoryManager.GetNextNode(current);

        if (nextBuff.IsEnough(GetTotalAmount()))
        {
            current = nextBuff;
            SetBuffIndex(buffIndex + 1);
            OnArtifactBuffAdd?.Invoke(current);
        }
    }

    private void SetBuffIndex(int index)
    {
        buffIndex = index;
        buffIndex = Mathf.Clamp(buffIndex, -1, ArtifactEffectFactoryManager.GetTotalPieceBuffs() - 1);
    }

    public void Remove(Artifact artifact)
    {
        _artifacts.Remove(artifact);

        if (current != null && !current.IsEnough(GetTotalAmount()))
        {
            ArtifactBuffInformation prevBuff = current;
            current = current.parentNode;
            SetBuffIndex(buffIndex - 1);
            OnArtifactBuffRemove?.Invoke(prevBuff);
        }

        if (GetTotalAmount() == 0)
        {
            OnFamilyRemove?.Invoke(this);
        }
    }

    public int GetBuffCurrentIndex()
    {
        return buffIndex;
    }
}
