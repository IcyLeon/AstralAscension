using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[DisallowMultipleComponent]
public class ArtifactBubbleManager : MonoBehaviour
{
    [Range(0.25f, 1f)]
    [SerializeField] private float distanceFromCharacter = 0.5f;
    public ArtifactInventory currentArtifactInventory { get; private set; }
    public event Action OnArtifactInventoryChanged;
    public delegate void OnArtifactEquippedEvent(Artifact Artifact);
    public event OnArtifactEquippedEvent OnArtifactEquip;
    public event OnArtifactEquippedEvent OnArtifactUnEquip;
    public event Action<ArtifactBubble> OnArtifactBubbleSelected;

    private Dictionary<ItemTypeSO, ArtifactBubble> artifactBubbleDic;

    private void Awake()
    {
        InitSpheres();
    }

    public void SetArtifactInventory(ArtifactInventory inventory)
    {
        if (currentArtifactInventory == inventory)
            return;

        OnArtifactInventoryUnsubscribeEvents();
        currentArtifactInventory = inventory;
        OnArtifactInventorySubscribeEvents();
    }

    private void OnArtifactInventoryUnsubscribeEvents()
    {
        if (currentArtifactInventory == null)
            return;

        currentArtifactInventory.OnArtifactEquip -= ArtifactInventory_OnArtifactEquip;
        currentArtifactInventory.OnArtifactUnEquip -= ArtifactInventory_OnArtifactUnEquip;
    }


    private void OnArtifactInventorySubscribeEvents()
    {
        if (currentArtifactInventory == null)
            return;

        currentArtifactInventory.OnArtifactEquip += ArtifactInventory_OnArtifactEquip;
        currentArtifactInventory.OnArtifactUnEquip += ArtifactInventory_OnArtifactUnEquip;
        OnArtifactInventoryChanged?.Invoke();
        InitEquipAllArtifacts();
    }

    private void InitEquipAllArtifacts()
    {
        if (currentArtifactInventory == null)
            return;

        Dictionary<ArtifactFamilySO, ArtifactFamily> artifactList = currentArtifactInventory.artifactList;

        foreach (var artifactFamily in artifactList.Values)
        {
            for (int i = 0; i < artifactFamily._artifacts.Count; i++)
            {
                Artifact artifact = artifactFamily._artifacts[i];
                ArtifactInventory_OnArtifactEquip(artifact);
            }
        }
    }


    private void ArtifactInventory_OnArtifactUnEquip(Artifact Artifact)
    {
        OnArtifactUnEquip?.Invoke(Artifact);
    }

    private void ArtifactInventory_OnArtifactEquip(Artifact Artifact)
    {
        OnArtifactEquip?.Invoke(Artifact);
    }

    private void InitSpheres()
    {
        if (artifactBubbleDic != null)
            return;

        artifactBubbleDic = new();

        ArtifactBubble[] artifactBubbleList = GetComponentsInChildren<ArtifactBubble>(true);

        for (int i = 0; i < artifactBubbleList.Length; i++)
        {
            ArtifactBubble artifactBubble = artifactBubbleList[i];

            if (Contains(artifactBubble.ArtifactTypeSO))
                continue;

            artifactBubbleDic.Add(artifactBubble.ArtifactTypeSO, artifactBubble);
        }

        SubscribeEvents();
    }

    private bool Contains(ItemTypeSO ArtifactTypeSO)
    {
        return artifactBubbleDic.ContainsKey(ArtifactTypeSO);
    }

    private void SubscribeEvents()
    {
        for (int i = 0; i < artifactBubbleDic.Values.Count; i++)
        {
            ArtifactBubble artifactBubble = artifactBubbleDic.ElementAt(i).Value;
            Vector3 vec = GetVectorXZ(GetAngle(i));
            artifactBubble.transform.localPosition = vec * distanceFromCharacter;
            artifactBubble.OnArtifactSphereSelect += ArtifactSphereManager_OnArtifactSphereSelect;
        }
    }

    private void UnsubscribeEvents()
    {
        OnArtifactInventoryUnsubscribeEvents();

        foreach (var artifactBubble in artifactBubbleDic.Values)
        {
            artifactBubble.OnArtifactSphereSelect -= ArtifactSphereManager_OnArtifactSphereSelect;
        }

        artifactBubbleDic.Clear();
    }

    private void ArtifactSphereManager_OnArtifactSphereSelect(ArtifactBubble ArtifactBubble)
    {
        SelectArtifactBubble(ArtifactBubble);
    }

    public void SelectArtifactBubble(ItemTypeSO ItemTypeSO)
    {
        InitSpheres();

        if (!Contains(ItemTypeSO))
            return;

        SelectArtifactBubble(artifactBubbleDic[ItemTypeSO]);
    }

    private void SelectArtifactBubble(ArtifactBubble ArtifactBubble)
    {
        OnArtifactBubbleSelected?.Invoke(ArtifactBubble);
    }

    private Vector3 GetVectorXZ(float angle)
    {
        return Vector3Handler.FindVector(angle, 0);
    }
    private float GetAngle(int noOfSlice)
    {
        float angle = 360f / artifactBubbleDic.Count;
        return angle * noOfSlice;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
