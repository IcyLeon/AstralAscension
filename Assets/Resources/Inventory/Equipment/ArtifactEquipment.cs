using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental;
using UnityEngine;

public class ArtifactEquipment : CharacterEquipment
{
    public Dictionary<ArtifactFamilySO, ArtifactFamily> artifactFamilyList { get; } = new();
    public readonly Dictionary<ItemTypeSO, Artifact> artifactList = new();
    public delegate void OnArtifactFamilyEvent(ArtifactFamily ArtifactFamily);
    public event OnArtifactFamilyEvent OnArtifactFamilyAdd;
    public event OnArtifactFamilyEvent OnArtifactFamilyRemove;

    public delegate void OnArtifactEquippedEvent(Artifact Artifact);
    public event OnArtifactEquippedEvent OnArtifactEquip;
    public event OnArtifactEquippedEvent OnArtifactUnEquip;

    public ArtifactEquipment() : base()
    {
    }

    public override void Equip(IData Item)
    {
        Artifact artifact = Item as Artifact;
        ArtifactFamilySO artifactFamilySO = artifact.artifactSO.ArtifactFamilySO;

        if (!artifactFamilyList.ContainsKey(artifactFamilySO))
        {
            ArtifactFamily ArtifactFamily = artifactFamilySO.CreateArtifactFamily();
            ArtifactFamily.OnFamilyRemove += ArtifactFamily_OnFamilyRemove;
            artifactFamilyList.Add(artifactFamilySO, ArtifactFamily);
            OnArtifactFamilyAdd?.Invoke(ArtifactFamily);
        }

        artifactFamilyList[artifactFamilySO].Add(artifact);
        artifactList.Add(Item.GetTypeSO(), artifact);
        OnArtifactEquip?.Invoke(artifact);
    }


    public override void UnEquip(IData Item)
    {
        Artifact Artifact = Item as Artifact;
        artifactFamilyList[Artifact.artifactSO.ArtifactFamilySO].Remove(Artifact);
        artifactList.Remove(Item.GetTypeSO());
        OnArtifactUnEquip?.Invoke(Artifact);
    }

    private void ArtifactFamily_OnFamilyRemove(ArtifactFamily ArtifactFamily)
    {
        ArtifactFamily.OnFamilyRemove -= ArtifactFamily_OnFamilyRemove;
        artifactFamilyList.Remove(ArtifactFamily.artifactFamilySO);
        OnArtifactFamilyRemove?.Invoke(ArtifactFamily);
    }

    public override IData GetExistingItem(IData Item)
    {
        if (artifactList.TryGetValue(Item.GetTypeSO(), out Artifact Artifact))
            return Artifact;

        return null;
    }
}
