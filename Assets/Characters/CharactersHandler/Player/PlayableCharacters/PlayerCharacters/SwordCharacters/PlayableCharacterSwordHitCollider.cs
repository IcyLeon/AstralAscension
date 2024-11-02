using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using static Unity.VisualScripting.Member;

[RequireComponent(typeof(Collider))]
public class PlayableCharacterSwordHitCollider : MonoBehaviour
{
    public class PlayableCharacterHitEvents : EventArgs
    {
        public IDamageable damageable;
        public IDamageable source;
        public Vector3 hitPosition;
    }

    private IDamageable Source;
    [SerializeField] private Collider hitCollider;
    public event EventHandler<PlayableCharacterHitEvents> EntitySwordHitEvent;

    private void Awake()
    {
        Source = GetComponentInParent<IDamageable>();
        hitCollider.enabled = false;
    }

    /// <summary>
    /// Turn on the collider for 1 frame to call OnTriggerEnter
    /// </summary>
    private IEnumerator HitCoroutine()
    {
        hitCollider.enabled = true;
        yield return null;
        hitCollider.enabled = false;
    }

    public void EnableCollider()
    {
        StartCoroutine(HitCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetType() == Source.GetType())
            return;

        if (other.TryGetComponent(out IDamageable damageable))
        {
            EntitySwordHitEvent?.Invoke(this, new PlayableCharacterHitEvents
            {
                damageable = damageable,
                source = Source,
                hitPosition = other.ClosestPoint(transform.position)
            });
        }
    }
}
