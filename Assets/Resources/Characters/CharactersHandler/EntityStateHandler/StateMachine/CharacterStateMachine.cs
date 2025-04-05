using UnityEngine;

public abstract class CharacterStateMachine : StateMachine
{
    protected StateMachineManager StateMachineManager;
    public Characters characters { get; }

    /// <summary>
    /// Starting State for entities
    /// </summary>
    public CharacterReuseableData characterReuseableData { get; protected set; }

    public override void Update()
    {
        if (characterReuseableData != null)
            characterReuseableData.Update();

        StateMachineManager.Update();
    }

    public bool IsInState<T>()
    {
        return StateMachineManager.IsInState<T>();
    }
    public override void FixedUpdate()
    {
        base.FixedUpdate();
        StateMachineManager.FixedUpdate();
    }

    public override void LateUpdate()
    {
        base.LateUpdate();
        StateMachineManager.LateUpdate();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        StateMachineManager.OnEnable();
        StateMachineManager.ResetState();
    }

    public override void OnDisable()
    {
        base.OnDisable();
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
    protected void StartState(IState newState)
    {
        StateMachineManager.StartState(newState);
    }

    public CharacterStateMachine(Characters characters)
    {
        StateMachineManager = new StateMachineManager();
        this.characters = characters;
        InitComponent();
    }

    protected virtual void InitComponent()
    {

    }

    public override void OnDestroy()
    {
        base.OnDestroy();
        if (characterReuseableData != null)
            characterReuseableData.OnDestroy();
    }

    public void StartAnimation(string parameter)
    {
        Characters.StartAnimation(characters.Animator, parameter);
    }

    public bool IsInTransition()
    {
        return characters.Animator.IsInTransition(characters.Animator.GetLayerIndex("Base Layer"));
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
