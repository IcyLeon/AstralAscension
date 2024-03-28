using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Characters : MonoBehaviour, IDamageable
{
    protected CharacterStateMachine characterStateMachine;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.Update();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.FixedUpdate();
        }
    }

    public static void StartAnimation(Animator animator, string HashParameter)
    {
        if (animator == null)
            return;

        if (CharacterManager.ContainsParam(animator, HashParameter))
            animator.SetBool(HashParameter, true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.OnCollisionEnter(collision);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.OnCollisionStay(collision);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.OnCollisionExit(collision);
        }
    }

    public virtual void TakeDamage(IDamageable source, float BaseDamageAmount)
    {
    }
}
