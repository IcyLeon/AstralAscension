using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairpinExplosion : MonoBehaviour
{
    [SerializeField] private Collider ExplosionCollider;
    private HairpinTeleporter m_Teleporter;

    // Start is called before the first frame update
    void Awake()
    {
        m_Teleporter = GetComponentInParent<HairpinTeleporter>();
        m_Teleporter.OnHairPinExplode += M_Teleporter_OnHairPinExplode;
    }

    private void M_Teleporter_OnHairPinExplode()
    {
        StartCoroutine(HitCoroutine());
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayableCharacters pc = other.GetComponent<PlayableCharacters>();

        if (pc != null)
            return;

        if (other.TryGetComponent(out IDamageable damageable))
        {
            ElementsSO elementSO = null;
            IAttacker source = m_Teleporter.source;
            if (source != null)
            {
                elementSO = source.GetElementsSO();
            }

            damageable.TakeDamage(source, elementSO, 1f, other.ClosestPoint(transform.position));
        }
    }

    private IEnumerator HitCoroutine()
    {
        ExplosionCollider.enabled = true;
        yield return null;
        ExplosionCollider.enabled = false;
    }

    private void Start()
    {
        ExplosionCollider.enabled = false;
    }

    private void OnDestroy()
    {

        if (m_Teleporter != null)
        {
            m_Teleporter.OnHairPinExplode -= M_Teleporter_OnHairPinExplode;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
