using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ArtifactSphereManager : MonoBehaviour
{
    [Range(0.25f, 1f)]
    [SerializeField] private float distanceFromCharacter = 0.5f;
    private ArtifactSphere[] artifactSphereList;
    public ArtifactInventory currentArtifactInventory { get; private set; }
    public event Action OnArtifactInventoryChanged;
    public delegate void OnArtifactEquippedEvent(Artifact Artifact);
    public event OnArtifactEquippedEvent OnArtifactEquip;
    public event OnArtifactEquippedEvent OnArtifactUnEquip;

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
        Dictionary<ArtifactFamilySO, ArtifactFamily> artifactList = currentArtifactInventory.artifactList;

        foreach (var artifactFamilyKeys in artifactList)
        {
            ArtifactFamily ArtifactFamily = artifactFamilyKeys.Value;
            for (int i = 0; i < ArtifactFamily._artifacts.Count; i++)
            {
                Artifact artifact = ArtifactFamily._artifacts[i];
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
        artifactSphereList = GetComponentsInChildren<ArtifactSphere>(true);

        for (int i = 0; i < artifactSphereList.Length; i++)
        {
            Vector3 vec = GetVectorXZ(GetAngle(i));
            artifactSphereList[i].transform.localPosition = vec * distanceFromCharacter;
        }

        SubscribeEvents();
    }

    private void SubscribeEvents()
    {
        for (int i = 0; i < artifactSphereList.Length; i++)
        {
            artifactSphereList[i].OnArtifactSphereSelect += ArtifactSphereManager_OnArtifactSphereSelect;
        }
    }

    private void UnsubscribeEvents()
    {
        for (int i = 0; i < artifactSphereList.Length; i++)
        {
            artifactSphereList[i].OnArtifactSphereSelect -= ArtifactSphereManager_OnArtifactSphereSelect;
        }
    }

    private void ArtifactSphereManager_OnArtifactSphereSelect(ArtifactSphere obj)
    {
        Debug.Log(obj);
    }

    private Vector3 GetVectorXZ(float angle)
    {
        return Vector3Handler.FindVector(angle, 0);
    }
    private float GetAngle(int noOfSlice)
    {
        float angle = 360f / artifactSphereList.Length;
        return angle * noOfSlice;
    }

    private void OnDestroy()
    {
        OnArtifactInventoryUnsubscribeEvents();
        UnsubscribeEvents();
    }
}
