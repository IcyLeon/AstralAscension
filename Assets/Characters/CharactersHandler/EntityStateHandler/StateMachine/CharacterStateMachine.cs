using UnityEngine;

public abstract class CharacterStateMachine
{
    protected StateMachineManager StateMachineManager;
    public Characters characters { get; }
    public EntityState EntityState { get; protected set; }
    public CharacterReuseableData characterReuseableData { get; protected set; }

    public virtual void Update()
    {
        StateMachineManager.Update();
    }

    protected virtual void InitState()
    {
    }

    public bool IsInState<T>()
    {
        return StateMachineManager.IsInState<T>();
    }
    public virtual void FixedUpdate()
    {
        StateMachineManager.FixedUpdate();
    }

    public virtual void LateUpdate()
    {
        StateMachineManager.LateUpdate();
    }

    public virtual void OnEnable()
    {
        StateMachineManager.OnEnable();
    }

    public virtual void OnDisable()
    {
        StateMachineManager.OnDisable();

        if (characterReuseableData != null)
            characterReuseableData.ResetData();
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
    public IState GetCurrentState()
    {
        return StateMachineManager.currentStates;
    }

    public CharacterStateMachine(Characters characters)
    {
        StateMachineManager = new StateMachineManager();
        this.characters = characters;
        InitState();
        ChangeState(EntityState);
    }
    public virtual void OnDestroy()
    {
        if (characterReuseableData != null)
            characterReuseableData.OnDestroy();
    }

    public void StartAnimation(string parameter)
    {
        Characters.StartAnimation(characters.Animator, parameter);
    }

    public void SetAnimationTrigger(string parameter)
    {
        Characters.SetAnimationTrigger(characters.Animator, parameter);
    }

    public void StopAnimation(string parameter)
    {
        Characters.StopAnimation(characters.Animator, parameter);
    }
}
