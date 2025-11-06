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
    public ArtifactEquipment currentArtifactInventory { get; private set; }
    public event Action OnArtifactInventoryChanged;
    public delegate void OnArtifactEquippedEvent(Artifact Artifact);
    public event OnArtifactEquippedEvent OnArtifactEquip;
    public event OnArtifactEquippedEvent OnArtifactUnEquip;
    public event Action<ArtifactBubble> OnArtifactBubbleSelected;
    private SelectedArtifactBubble selectedArtifactBubble;
    private ArtifactsRingRotation artifactsRingRotation;
    private Dictionary<ItemTypeSO, ArtifactBubble> artifactBubbleDic;
    private Coroutine scaleAnimationCoroutine;

    private void Awake()
    {
        InitSpheres();
        artifactsRingRotation = GetComponent<ArtifactsRingRotation>();
    }

    public void EnableRotation()
    {
        if (artifactsRingRotation == null)
            return;

        artifactsRingRotation.EnableRotation();
    }

    private void OnEnable()
    {
        PlayScaleAnimation();
    }

    private void PlayScaleAnimation()
    {
        if (!gameObject.activeInHierarchy)
            return;

        float AnimationTime = 0.45f;

        if (scaleAnimationCoroutine != null)
        {
            StopCoroutine(scaleAnimationCoroutine);
        }

        scaleAnimationCoroutine = StartCoroutine(ScaleAnimation(AnimationTime));
    }

    public void ShowArtifactsBubble()
    {
        gameObject.SetActive(true);
    }

    public void HideArtifactsBubble()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator ScaleAnimation(float AnimationTime)
    {
        float dt = 0f;
        float peakStrength = 0.15f;

        while (dt < AnimationTime)
        {
            float incrementScale = MathF.Sin((-MathF.PI / 2f) + Mathf.PI * ((dt * 1.5f) / AnimationTime));

            if (incrementScale > 0f)
            {
                incrementScale *= peakStrength;
            }

            transform.localScale = Vector3.one * (1f + incrementScale);
            dt += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.localScale = Vector3.one;
    }


    public void DisableRotation()
    {
        if (artifactsRingRotation == null)
            return;

        artifactsRingRotation.DisableRotation();
    }

    public void EnableSelectedBubble()
    {
        if (selectedArtifactBubble == null)
        {
            selectedArtifactBubble = gameObject.AddComponent<SelectedArtifactBubble>();
        }

        selectedArtifactBubble.enabled = true;
    }

    public void DisableSelectedBubble()
    {
        if (selectedArtifactBubble == null)
        {
            return;
        }
        selectedArtifactBubble.enabled = false;
    }

    public void SetArtifactInventory(ArtifactEquipment inventory)
    {
        if (currentArtifactInventory == inventory)
            return;

        OnArtifactInventoryUnsubscribeEvents();
        currentArtifactInventory = inventory;
        OnArtifactInventorySubscribeEvents();

        PlayScaleAnimation();
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
            foreach (var _artifact in artifactFamily._artifacts.Values)
            {
                ArtifactInventory_OnArtifactEquip(_artifact);
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
        if (!CanSelectArtifactBubble())
            return;

        ToggleAllArtifactBubble(false);
        ArtifactBubble.gameObject.SetActive(true);
        OnArtifactBubbleSelected?.Invoke(ArtifactBubble);
    }

    private bool CanSelectArtifactBubble()
    {
        return selectedArtifactBubble != null && selectedArtifactBubble.enabled;
    }

    public void ToggleAllArtifactBubble(bool active)
    {
        foreach(var artifactBubble in artifactBubbleDic.Values)
        {
            artifactBubble.gameObject.SetActive(active);
        }
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
