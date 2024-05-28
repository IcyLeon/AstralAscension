using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairpinTeleporter : MonoBehaviour
{
    private float TimeToDisappear = 5f;
    private float TimeToDisappearElapsed;

    private Transform ParentTransform;
    public IAttacker source { get; private set; }
    [SerializeField] private float Speed;
    [SerializeField] private AudioSource ThrowAudioSource;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Collider PinCollider;
    private Coroutine MoveTarget;

    public event Action OnHairPinExplode;

    public void SetTargetLocation(Vector3 Position)
    {
        gameObject.SetActive(true);
        ResetMovement();
        MoveTarget = StartCoroutine(MoveToTarget(Position));
    }

    private IEnumerator MoveToTarget(Vector3 TargetPosition)
    {
        while ((rb.position - TargetPosition).magnitude >= 0.1f)
        {
            Vector3 pos = Vector3.MoveTowards(rb.position, TargetPosition, Speed * Time.deltaTime);
            rb.MovePosition(pos);
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
        return MoveTarget == null && gameObject.activeSelf;
    }

    public void Hide()
    {
        transform.SetParent(ParentTransform);
        gameObject.SetActive(false);
    }

    private void OnEnable()
    {
        if (source != null)
            transform.SetParent(null);

        PinCollider.enabled = true;
        ResetTime();
    }

    private void OnDisable()
    {
        ResetMovement();
        ResetTime();
    }

    public void Init(IAttacker source, Transform transform)
    {
        this.source = source;
        ParentTransform = transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Ignore Raycast"))
            return;

        PlayableCharacters pc = other.GetComponent<PlayableCharacters>();

        if (pc != null)
            return;

        Explode();
    }

    private void Explode()
    {
        PinCollider.enabled = false;
        ThrowAudioSource.Play();
        OnHairPinExplode?.Invoke();
        ResetMovement();
    }

    private void ResetMovement()
    {
        if (MoveTarget != null)
        {
            StopCoroutine(MoveTarget);
        }
        MoveTarget = null;
    }
}
