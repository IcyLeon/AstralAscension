using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Characters : MonoBehaviour, IDamageable, IPointOfInterest
{
    [SerializeField] private Transform POITargetTransform;
    [field: SerializeField] public Animator Animator { get; private set; }
    [SerializeField] private AudioSource VoiceSource;
    public Transform closestInteractionTransform { get; protected set; }
    protected CharacterStateMachine characterStateMachine;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {

    }

    public Transform GetIPointOfInterestTransform()
    {
        return POITargetTransform;
    }

    protected void UpdateInteractionTransform(Transform transform)
    {
        closestInteractionTransform = transform;
    }

    public void PlayVOAudio(AudioClip clip)
    {
        if (clip == null)
            return;

        VoiceSource.PlayOneShot(clip);
    }

    protected virtual void OnEnable()
    {
    }

    protected virtual void OnDisable()
    {

    }

    public virtual bool IsDead()
    {
        return false;
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

    public void OnCharacterAnimationTransition()
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.OnAnimationTransition();
        }
    }

    protected virtual void FixedUpdate()
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.FixedUpdate();
        }
    }

    protected virtual void LateUpdate()
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.LateUpdate();
        }
    }

    public static void StartAnimation(Animator animator, string HashParameter)
    {
        if (animator == null)
            return;

        if (CharacterManager.ContainsParam(animator, HashParameter))
            animator.SetBool(HashParameter, true);
    }

    public static void SetAnimationTrigger(Animator animator, string HashParameter)
    {
        if (animator == null)
            return;

        if (CharacterManager.ContainsParam(animator, HashParameter))
            animator.SetTrigger(HashParameter);
    }

    public static void StopAnimation(Animator animator, string HashParameter)
    {
        if (animator == null)
            return;

        if (CharacterManager.ContainsParam(animator, HashParameter))
            animator.SetBool(HashParameter, false);
    }

    public virtual void TakeDamage(IDamageable source, float BaseDamageAmount)
    {
    }
}
