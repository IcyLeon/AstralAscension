using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayableCharacterSwordHitCollider : MonoBehaviour
{
    [SerializeField] private PlayableCharacters playableCharacters;
    [SerializeField] private Collider hitCollider;

    private void Start()
    {
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
        if (other.gameObject == playableCharacters.gameObject)
            return;

        PlayableCharacters pc = other.GetComponent<PlayableCharacters>();

        if (pc != null)
            return;

        if (other.TryGetComponent(out IDamageable damageable))
        {
            damageable.TakeDamage(playableCharacters, playableCharacters.GetElementsSO(), 1f);
        }
    }
}
