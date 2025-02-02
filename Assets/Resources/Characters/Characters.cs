using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[DisallowMultipleComponent]
public abstract class Characters : MonoBehaviour
{
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
    private void Update()
    {
        if (Time.timeScale == 0)
            return;

        OnUpdate();
    }

    protected virtual void OnUpdate()
    {

    }

    protected virtual void OnFixedUpdate()
    {

    }

    protected virtual void OnLateUpdate()
    {

    }

    private void FixedUpdate()
    {
        if (Time.timeScale == 0)
            return;

        OnFixedUpdate();
    }

    private void LateUpdate()
    {
        if (Time.timeScale == 0)
            return;

        OnLateUpdate();
    }

    public static void StartAnimation(Animator animator, string HashParameter)
    {
        if (animator == null)
            return;

        if (CharacterManager.ContainsParam(animator, HashParameter))
        {
            animator.SetBool(HashParameter, true);
        }
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
