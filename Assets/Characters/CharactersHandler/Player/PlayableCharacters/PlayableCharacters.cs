using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacters : Characters
{
    [field: SerializeField] public PlayerCharactersSO PlayerCharactersSO { get; private set; }

    [field: SerializeField] public CapsuleCollider MainCollider { get; private set; }

    [field: SerializeField] public Collider BasicAttackCollider { get; private set; }

    [field: SerializeField] public AimRig POIRig { get; private set; }

    public Player player { get; private set; }

    public PlayableCharacterAnimationSO PlayableCharacterAnimationSO
    {
        get
        {
            return PlayerCharactersSO.PlayableCharacterAnimationSO;
        }
    }

    public PlayableCharacterStateMachine PlayableCharacterStateMachine
    {
        get
        {
            return characterStateMachine as PlayableCharacterStateMachine;
        }
    }

    public void OnPlayerAnimationTransition()
    {
        if (PlayableCharacterStateMachine.playerStateMachine != null)
        {
            PlayableCharacterStateMachine.playerStateMachine.OnAnimationTransition();
        }
    }

    protected override void Awake()
    {
        base.Awake();
        player = transform.parent.GetComponent<Player>();
    }

    private void OnPlungeATK(Vector3 position)
    {

    }

    protected override void OnEnable()
    {
        base.OnEnable();
        player.OnCollisionEnterEvent += OnCollisionEnterEvent;
        player.OnCollisionStayEvent += OnCollisionStayEvent;
        player.OnCollisionExitEvent += OnCollisionExitEvent;
        PlayerPlungeState.OnPlungeAction += OnPlungeATK;
        player.Interact.OnInteractEnter += OnInteractEnter;
        player.Interact.OnInteractExit += OnInteractExit;
    }

    private void OnInteractEnter(Collider collider)
    {
        OnInteractionEnter?.Invoke(collider);
    }

    private void OnInteractExit(Collider collider)
    {
        OnInteractionExit?.Invoke(collider);
    }

    protected override void OnDisable()
    {
        base.OnDisable();
        UnsubscribeEvents();
    }

    protected override void OnDestroy()
    {
        base.OnDestroy();
        UnsubscribeEvents();
    }

    private void UnsubscribeEvents()
    {
        player.OnCollisionEnterEvent -= OnCollisionEnterEvent;
        player.OnCollisionStayEvent -= OnCollisionStayEvent;
        player.OnCollisionExitEvent -= OnCollisionExitEvent;
        PlayerPlungeState.OnPlungeAction -= OnPlungeATK;
        player.Interact.OnInteractEnter -= OnInteractEnter;
        player.Interact.OnInteractExit -= OnInteractExit;
    }

    protected override void Start()
    {
        base.Start();
    }

    //private void OnDrawGizmosSelected()
    //{
    //    Color transparentGreen = new Color(0.0f, 1.0f, 0.0f, 0.35f);
    //    Gizmos.color = transparentGreen;

    //    // when selected, draw a gizmo in the position of, and matching radius of, the grounded collider
    //    Gizmos.DrawSphere(
    //        new Vector3(player.Rb.position.x, player.Rb.position.y + MainCollider.radius / 2f, player.Rb.position.z),
    //        MainCollider.radius);
    //}

    public void PlayVOAudio(AudioClip clip)
    {
        if (clip == null)
            return;

        PlayVOClip(clip);
    }

    protected override void Update()
    {
        base.Update();
    }

    private void OnCollisionEnterEvent(Collision collision)
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.OnCollisionEnter(collision);
        }
    }

    private void OnCollisionStayEvent(Collision collision)
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.OnCollisionStay(collision);
        }
    }

    private void OnCollisionExitEvent(Collision collision)
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.OnCollisionExit(collision);
        }
    }
}
