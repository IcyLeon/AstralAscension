using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtifactInventory
{
    public CharactersSO charactersSO { get; }
    private CharacterInventory inventory;
    public Dictionary<ArtifactFamilySO, ArtifactFamily> artifactList { get; }
    public delegate void OnArtifactFamilyEvent(ArtifactFamily ArtifactFamily);
    public event OnArtifactFamilyEvent OnArtifactFamilyAdd;
    public event OnArtifactFamilyEvent OnArtifactFamilyRemove;

    public delegate void OnArtifactEquippedEvent(Artifact Artifact);
    public event OnArtifactEquippedEvent OnArtifactEquip;
    public event OnArtifactEquippedEvent OnArtifactUnEquip;

    public ArtifactInventory(CharacterInventory CharacterInventory)
    {
        artifactList = new();
        inventory = CharacterInventory;
        charactersSO = inventory.charactersSO;
        inventory.OnEquip += Inventory_OnEquip;
        inventory.OnUnEquip += Inventory_OnUnEquip;
    }

    private void ArtifactFamily_OnFamilyRemove(ArtifactFamily ArtifactFamily)
    {
        ArtifactFamily.OnFamilyRemove -= ArtifactFamily_OnFamilyRemove;
        artifactList.Remove(ArtifactFamily.artifactFamilySO);
        OnArtifactFamilyRemove?.Invoke(ArtifactFamily);
    }

    private void Inventory_OnUnEquip(UpgradableItems UpgradableItem)
    {
        Artifact Artifact = UpgradableItem as Artifact;

        if (Artifact == null)
            return;

        Debug.Log("Unequipped");
        artifactList[Artifact.artifactSO.ArtifactFamilySO].Remove(Artifact);
        OnArtifactUnEquip?.Invoke(Artifact);
    }

    private void Inventory_OnEquip(UpgradableItems UpgradableItem)
    {
        Artifact Artifact = UpgradableItem as Artifact;

        if (Artifact == null)
            return;

        ArtifactFamilySO artifactFamilySO = Artifact.artifactSO.ArtifactFamilySO;

        if (!artifactList.ContainsKey(artifactFamilySO))
        {
            ArtifactFamily ArtifactFamily = artifactFamilySO.CreateArtifactFamily();
            ArtifactFamily.OnFamilyRemove += ArtifactFamily_OnFamilyRemove;
            artifactList.Add(artifactFamilySO, ArtifactFamily);
            OnArtifactFamilyAdd?.Invoke(ArtifactFamily);
        }

        artifactList[artifactFamilySO].Add(Artifact);
        OnArtifactEquip?.Invoke(Artifact);
    }

    public void OnDestroy()
    {
        inventory.OnEquip -= Inventory_OnEquip;
        inventory.OnUnEquip -= Inventory_OnUnEquip;
    }
}
