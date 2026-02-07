using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtTargetOffset : MonoBehaviour
{
    [Range(0f, 1f)]
    [SerializeField] private float AnimationTime;
    private LookAtTargetOffsetData lookAtTargetOffsetData;
    private Coroutine transitionAnimation;


    private void OnEnable()
    {
        CharacterDisplayMiscEvent.OnCharacterShowcase += CharacterDisplayMiscEvent_OnCharacterShowcase;
    }

    private void CharacterDisplayMiscEvent_OnCharacterShowcase(CharactersSO CharactersSO, ModelsHandler ModelsHandler)
    {
        lookAtTargetOffsetData = ModelsHandler.GetComponentInChildren<LookAtTargetOffsetData>();
        StartAnimation(lookAtTargetOffsetData.GetStartingPosition());
    }

    private IEnumerator PositionAnimation(Vector3 StartingPosition)
    {
        float animationTime = AnimationTime;
        float dt = 0;

        while (dt < animationTime)
        {
            transform.position = Vector3.Lerp(transform.position, StartingPosition, dt / animationTime);
            dt += Time.unscaledDeltaTime;
            yield return null;
        }

        transform.position = StartingPosition;
    }

    private void StartAnimation(Vector3 StartingPosition)
    {
        if (transitionAnimation != null)
            StopCoroutine(transitionAnimation);

        transitionAnimation = StartCoroutine(PositionAnimation(StartingPosition));
    }

    private void OnDisable()
    {
        CharacterDisplayMiscEvent.OnCharacterShowcase -= CharacterDisplayMiscEvent_OnCharacterShowcase;
    }
}
