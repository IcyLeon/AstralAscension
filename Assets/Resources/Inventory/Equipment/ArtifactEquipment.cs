using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactEquipment : CharacterEquipment
{
    public Dictionary<ArtifactFamilySO, ArtifactFamily> artifactList { get; } = new();
    public delegate void OnArtifactFamilyEvent(ArtifactFamily ArtifactFamily);
    public event OnArtifactFamilyEvent OnArtifactFamilyAdd;
    public event OnArtifactFamilyEvent OnArtifactFamilyRemove;

    public delegate void OnArtifactEquippedEvent(Artifact Artifact);
    public event OnArtifactEquippedEvent OnArtifactEquip;
    public event OnArtifactEquippedEvent OnArtifactUnEquip;

    public ArtifactEquipment()
    {
    }

    public override void Equip(IData Item)
    {
        Artifact Artifact = Item as Artifact;
        ArtifactFamilySO artifactFamilySO = Artifact.artifactSO.ArtifactFamilySO;

        if (GetArtifactFamily(artifactFamilySO) == null)
        {
            ArtifactFamily ArtifactFamily = artifactFamilySO.CreateArtifactFamily();
            ArtifactFamily.OnFamilyRemove += ArtifactFamily_OnFamilyRemove;
            artifactList.Add(artifactFamilySO, ArtifactFamily);
            OnArtifactFamilyAdd?.Invoke(ArtifactFamily);
        }

        artifactList[artifactFamilySO].Add(Artifact);
        OnArtifactEquip?.Invoke(Artifact);
    }

    private ArtifactFamily GetArtifactFamily(ArtifactFamilySO ArtifactFamilySO)
    {
        if (artifactList.TryGetValue(ArtifactFamilySO, out ArtifactFamily artifactFamily))
        {
            return artifactFamily;
        }

        return null;
    }

    public override void UnEquip(IData Item)
    {
        Artifact Artifact = Item as Artifact;
        artifactList[Artifact.artifactSO.ArtifactFamilySO].Remove(Artifact);
        OnArtifactUnEquip?.Invoke(Artifact);
    }

    private void ArtifactFamily_OnFamilyRemove(ArtifactFamily ArtifactFamily)
    {
        ArtifactFamily.OnFamilyRemove -= ArtifactFamily_OnFamilyRemove;
        artifactList.Remove(ArtifactFamily.artifactFamilySO);
        OnArtifactFamilyRemove?.Invoke(ArtifactFamily);
    }

    public override IData GetItem(IData Item)
    {
        Artifact Artifact = Item as Artifact;
        if (GetArtifactFamily(Artifact.artifactSO.ArtifactFamilySO) == null)
            return null;

        ArtifactFamily artifactFamily = artifactList[Artifact.artifactSO.ArtifactFamilySO];
        return artifactFamily.GetArtifact(Artifact.artifactSO);
    }
}
