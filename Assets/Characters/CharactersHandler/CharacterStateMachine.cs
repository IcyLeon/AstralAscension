using UnityEngine;

public abstract class CharacterStateMachine
{
    protected StateMachineManager StateMachineManager;
    public Characters characters { get; }
    public CharacterReuseableData characterReuseableData { get; protected set; }
    public virtual void Update()
    {
        StateMachineManager.Update();
    }

    public virtual void FixedUpdate()
    {
        StateMachineManager.FixedUpdate();
    }

    public virtual void LateUpdate()
    {
        StateMachineManager.LateUpdate();
    }

    public virtual void OnAnimationTransition()
    {
        StateMachineManager.OnAnimationTransition();
    }

    public virtual void OnCollisionEnter(Collision collision)
    {
        StateMachineManager.OnCollisionEnter(collision);
    }

    public virtual void OnCollisionExit(Collision collision)
    {
        StateMachineManager.OnCollisionExit(collision);
    }

    public virtual void OnCollisionStay(Collision collision)
    {
        StateMachineManager.OnCollisionStay(collision);
    }
    public virtual void OnTriggerEnter(Collider Collider)
    {
        StateMachineManager.OnTriggerEnter(Collider);
    }

    public virtual void OnTriggerExit(Collider Collider)
    {
        StateMachineManager.OnTriggerExit(Collider);
    }

    public virtual void OnTriggerStay(Collider Collider)
    {
        StateMachineManager.OnTriggerStay(Collider);
    }

    public void ChangeState(IState newState)
    {
        StateMachineManager.ChangeState(newState);
    }

    public CharacterStateMachine(Characters characters)
    {
        StateMachineManager = new StateMachineManager();
        this.characters = characters;

    }

    public void StartAnimation(string parameter)
    {
        Characters.StartAnimation(characters.Animator, parameter);
    }

    public void StopAnimation(string parameter)
    {
        Characters.StopAnimation(characters.Animator, parameter);
    }
}
