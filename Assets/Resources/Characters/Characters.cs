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
    private CharacterAnimationEvents characterAnimationEvents;

    // Start is called before the first frame update
    protected virtual void Awake()
    {
        characterAnimationEvents = GetComponentInChildren<CharacterAnimationEvents>();
    }

    protected virtual void OnAnimationMove(Vector3 deltaPosition)
    {

    }

    protected virtual void Start()
    {

    }

    protected virtual void OnEnable()
    {
        SubscribeAnimationEvents();
    }

    protected virtual void OnDisable()
    {
        UnsubscribeAnimationEvents();
    }

    private void SubscribeAnimationEvents()
    {
        if (characterAnimationEvents == null)
            return;

        characterAnimationEvents.OnMove += OnAnimationMove;
    }

    private void UnsubscribeAnimationEvents()
    {
        if (characterAnimationEvents == null)
            return;

        characterAnimationEvents.OnMove -= OnAnimationMove;
    }

    public void PlayVOAudio(AudioClip clip)
    {
        if (clip == null)
            return;

        VoiceSource.PlayOneShot(clip);
    }
    protected virtual void OnDestroy()
    {
        UnsubscribeAnimationEvents();
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

}
