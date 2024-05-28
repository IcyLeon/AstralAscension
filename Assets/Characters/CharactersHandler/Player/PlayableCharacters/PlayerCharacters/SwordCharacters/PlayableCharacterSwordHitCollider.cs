using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayableCharacterSwordHitCollider : MonoBehaviour
{
    private IDamageable Damageable;
    [SerializeField] private Collider hitCollider;

    private void Start()
    {
        Damageable = GetComponentInParent<IDamageable>();
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
        if (other.GetType() == Damageable.GetType())
            return;

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(Damageable, Damageable.GetElementsSO(), 1f, other.ClosestPoint(transform.position));
        }
    }
}
