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

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }

    protected virtual void OnDestroy()
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

    public virtual void TakeDamage(IDamageable source, float BaseDamageAmount)
    {
    }
}
