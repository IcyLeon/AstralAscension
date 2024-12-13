using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class ArtifactFamily
{
    private List<Artifact> _artifacts;
    private ArtifactEffectFactoryManager ArtifactEffectFactoryManager;
    public ArtifactFamilySO artifactFamilySO { get; private set; }
    private ArtifactBuffInformation currentBuffLocation;

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

        ArtifactBuffInformation nextBuff = GetNextNode();

        if (IsEnoughAmount(nextBuff))
        {
            currentBuffLocation = nextBuff;
            OnArtifactBuffAdd?.Invoke(currentBuffLocation);
        }
    }

    private ArtifactBuffInformation GetNextNode()
    {
        if (currentBuffLocation == null)
        {
            return ArtifactEffectFactoryManager.GetStartingNode();
        }

        return currentBuffLocation.nextNode;
    }


    private bool IsEnoughAmount(ArtifactBuffInformation node)
    {
        if (node == null)
            return false;

        return GetTotalAmount() >= node.buffPieceInfo.NoOfPiece; 
    }

    public void Remove(Artifact artifact)
    {
        _artifacts.Remove(artifact);

        while (currentBuffLocation != null)
        {
            if (IsEnoughAmount(currentBuffLocation))
                break;

            ArtifactBuffInformation prevBuff = currentBuffLocation;
            currentBuffLocation = currentBuffLocation.prevNode;
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

        ArtifactBuffInformation currentNode = currentBuffLocation;

        while (currentNode != null)
        {
            index++;
            currentNode = currentNode.prevNode;
        }

        return index;
    }
}
