using UnityEngine;

public class CharacterStateMachine
{
    private IState currentStates;
    public Characters characters { get; } 

    public virtual void Update()
    {
        if (currentStates != null)
            currentStates.Update();
    }
    public virtual void FixedUpdate()
    {
        if (currentStates != null)
            currentStates.FixedUpdate();
    }

    public void OnAnimationTransition()
    {
        if (currentStates != null)
            currentStates.OnAnimationTransition();
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        if (currentStates != null)
            currentStates.OnCollisionEnter(collision);
    }

    public virtual void OnCollisionExit(Collision collision)
    {
        if (currentStates != null)
            currentStates.OnCollisionExit(collision);
    }

    public virtual void OnCollisionStay(Collision collision)
    {
        if (currentStates != null)
            currentStates.OnCollisionStay(collision);
    }

    public virtual void ChangeState(IState newState)
    {
        if (currentStates != null)
            currentStates.Exit();

        currentStates = newState;

        currentStates.Enter();
    }

    public CharacterStateMachine(Characters characters)
    {
        this.characters = characters;

    }
}
