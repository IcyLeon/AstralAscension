using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour, IDamageable
{
    protected CharacterState characterState;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (characterState != null)
        {
            characterState.Update();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (characterState != null)
        {
            characterState.FixedUpdate();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (characterState != null)
        {
            characterState.OnCollisionEnter(collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (characterState != null)
        {
            characterState.OnCollisionStay(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (characterState != null)
        {
            characterState.OnCollisionExit(collision);
        }
    }

    public virtual void TakeDamage(IDamageable source, float BaseDamageAmount)
    {
    }
}
