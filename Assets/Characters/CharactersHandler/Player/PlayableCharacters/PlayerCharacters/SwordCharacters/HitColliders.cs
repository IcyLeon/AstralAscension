using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class HitColliders : MonoBehaviour
{
    [SerializeField] private Characters character;
    [SerializeField] private Collider HitCollider;

    private void Start()
    {
        HitCollider.enabled = false;
    }

    /// <summary>
    /// Turn on the collider for 1 frame to call OnTriggerEnter
    /// </summary>
    private IEnumerator HitCoroutine()
    {
        HitCollider.enabled = true;
        yield return null;
        HitCollider.enabled = false;
    }

    public void EnableCollider()
    {
        StartCoroutine(HitCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == character.gameObject)
            return;

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(character.GetComponent<IDamageable>(), 1f);
        }
    }
}