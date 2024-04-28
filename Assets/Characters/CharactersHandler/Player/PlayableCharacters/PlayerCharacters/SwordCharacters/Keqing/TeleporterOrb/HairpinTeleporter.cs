using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairpinTeleporter : MonoBehaviour
{
    private float TimeToDisappear = 5f;
    private float TimeToDisappearElapsed;
    private PlayableCharacters playableCharacters;
    [SerializeField] private float Speed;
    [SerializeField] private AudioSource ThrowAudioSource;
    [SerializeField] private Rigidbody rb;
    private Coroutine MoveTarget;

    public delegate void onHairPinThrow();
    public static event onHairPinThrow OnHairPinThrow;

    public void SetTargetLocation(Vector3 Position)
    {
        gameObject.SetActive(true);
        if (MoveTarget == null)
            MoveTarget = StartCoroutine(MoveToTarget(Position));
        OnHairPinThrow?.Invoke();
    }

    private IEnumerator MoveToTarget(Vector3 TargetPosition)
    {
        while ((rb.position - TargetPosition).magnitude >= 0.1f)
        {
            rb.position = Vector3.MoveTowards(rb.position, TargetPosition, Speed * Time.deltaTime);
            yield return null;
        }

        Explode();
    }
    public void ResetTime()
    {
        TimeToDisappearElapsed = Time.time;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Time.time - TimeToDisappearElapsed > TimeToDisappear)
        {
            Hide();
            return;
        }
    }
    public bool CanTeleport()
    {
        return MoveTarget == null && gameObject.activeInHierarchy;
    }

    public void Hide()
    {
        transform.SetParent(playableCharacters.transform);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (playableCharacters)
            transform.SetParent(null);
        ResetTime();
    }

    private void OnDisable()
    {
        ResetMovement();
        ResetTime();
    }

    public void SetPlayableCharacter(PlayableCharacters pc)
    {
        playableCharacters = pc;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
            return;

        if (other.TryGetComponent(out PlayableCharacters pc))
            return;

        Explode();
    }

    private void Explode()
    {
        ThrowAudioSource.Play();
        ResetMovement();
    }

    private void ResetMovement()
    {
        if (MoveTarget != null)
        {
            StopCoroutine(MoveTarget);
            MoveTarget = null;
        }
    }
}
