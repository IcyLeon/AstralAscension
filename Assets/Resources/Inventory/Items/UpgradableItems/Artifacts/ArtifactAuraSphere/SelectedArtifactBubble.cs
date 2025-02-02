using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SelectedArtifactBubble : MonoBehaviour
{
    private float rotateSpeed;
    private ArtifactBubbleManager artifactBubbleManager;
    private ArtifactsRingRotation artifactsRingRotation;
    private Coroutine RotateRingCoroutine;

    private void Awake()
    {
        rotateSpeed = 10f;
        artifactBubbleManager = GetComponent<ArtifactBubbleManager>();
        artifactsRingRotation = artifactBubbleManager.GetComponent<ArtifactsRingRotation>();
    }

    private void StopRotation()
    {
        if (artifactsRingRotation == null)
            return;

        artifactsRingRotation.enabled = false;
    }

    private void StartRotation()
    {
        if (artifactsRingRotation == null)
            return;

        artifactsRingRotation.enabled = true;
    }

    private void ArtifactBubbleManager_OnArtifactBubbleSelected(ArtifactBubble artifactBubble)
    {
        RotateRingTowardsTarget(artifactBubble);
    }

    private void RotateRingTowardsTarget(ArtifactBubble ArtifactBubble)
    {
        if (RotateRingCoroutine != null)
        {
            StopCoroutine(RotateRingCoroutine);
        }

        StopRotation();

        RotateRingCoroutine = StartCoroutine(RotateTowardsSelectedBubbleCoroutine(ArtifactBubble));
    }

    private IEnumerator RotateTowardsSelectedBubbleCoroutine(ArtifactBubble artifactBubble)
    {
        Vector3 targetAngle = artifactBubbleManager.transform.localRotation.eulerAngles;
        targetAngle.y = 360f - GetAngle(artifactBubble);
        Quaternion targetQuaternion = Quaternion.Euler(targetAngle);
        float currentTime = Time.time;
        float limitTime = 1f;

        while (Time.time - currentTime < limitTime)
        {
            Quaternion currentQuaternion = artifactBubbleManager.transform.localRotation;
            currentQuaternion = Quaternion.Lerp(currentQuaternion, targetQuaternion, Time.unscaledDeltaTime * rotateSpeed);
            artifactBubbleManager.transform.localRotation = currentQuaternion;
            yield return null;
        }

        artifactBubbleManager.transform.localRotation = targetQuaternion;
        RotateRingCoroutine = null;
    }


    private float GetAngle(ArtifactBubble ArtifactBubble)
    {
        Vector3 dir = ArtifactBubble.transform.localPosition;
        return Vector3Handler.FindAngleByDirection(Vector2.zero, new Vector2(dir.x, dir.z));
    }


    private void OnEnable()
    {
        SubscribeEvents();
    }

    private void OnDisable()
    {
        StartRotation();
        UnsubscribeEvents();
    }

    private void SubscribeEvents()
    {
        artifactBubbleManager.OnArtifactBubbleSelected += ArtifactBubbleManager_OnArtifactBubbleSelected;
    }

    private void UnsubscribeEvents()
    {
        artifactBubbleManager.OnArtifactBubbleSelected -= ArtifactBubbleManager_OnArtifactBubbleSelected;
    }

    private void OnDestroy()
    {
        UnsubscribeEvents();
    }
}
