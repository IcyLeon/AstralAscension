using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class Characters : MonoBehaviour, IPointOfInterest
{
    [SerializeField] private Transform POITargetTransform;
    [field: SerializeField] public Animator Animator { get; private set; }
    [field: SerializeField] public CharactersSO CharacterSO { get; private set; }
    [SerializeField] private AudioSource VoiceSource;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        
    }

    protected virtual void Start()
    {

    }

    protected virtual void OnEnable()
    {

    }

    protected virtual void OnDisable()
    {

    }

    public Transform GetIPointOfInterestTransform()
    {
        return POITargetTransform;
    }

    public void PlayVOAudio(AudioClip clip)
    {
        if (clip == null)
            return;

        VoiceSource.PlayOneShot(clip);
    }
    protected virtual void OnDestroy()
    {

    }

    // Update is called once per frame
    protected virtual void Update()
    {
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void LateUpdate()
    {
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
        {
            animator.ResetTrigger(HashParameter);
            animator.SetTrigger(HashParameter);
        }
    }

    public static void StopAnimation(Animator animator, string HashParameter)
    {
        if (animator == null)
            return;

        if (CharacterManager.ContainsParam(animator, HashParameter))
            animator.SetBool(HashParameter, false);
    }
}
