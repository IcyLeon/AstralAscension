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

    public ArtifactFamily(ArtifactFamilySO ArtifactFamilySO)
    {
        _artifacts = new();
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
            OnArtifactBuffAdd?.Invoke(current);
        }
    }

    public void Remove(Artifact artifact)
    {
        _artifacts.Remove(artifact);

        if (current != null && current.IsEnough(GetTotalAmount()))
        {
            ArtifactBuffInformation prevBuff = current;
            current = current.parentNode;
            OnArtifactBuffRemove?.Invoke(prevBuff);
        }

        if (GetTotalAmount() == 0)
        {
            OnFamilyRemove?.Invoke(this);
        }
    }

    public int GetBuffCurrentIndex()
    {
        int index = -1;

        ArtifactBuffInformation currentNode = current;

        while (currentNode != null)
        {
            index++;
            currentNode = currentNode.parentNode;
        }

        return index;
    }
}
