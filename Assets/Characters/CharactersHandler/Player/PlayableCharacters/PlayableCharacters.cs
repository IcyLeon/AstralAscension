using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class PlayableCharacters : DamageableCharacters
{
    [field: SerializeField] public CapsuleCollider MainCollider { get; private set; }

    public Player player { get; private set; }

    public PlayableCharacterAnimationSO PlayableCharacterAnimationSO
    {
        get
        {
            return PlayerCharactersSO.PlayableCharacterAnimationSO;
        }
    }

    public PlayerCharactersSO PlayerCharactersSO
    {
        get
        {
            return damageableCharacters as PlayerCharactersSO;
        }
    }

    public PlayableCharacterStateMachine PlayableCharacterStateMachine
    {
        get
        {
            return characterStateMachine as PlayableCharacterStateMachine;
        }
    }

    protected override void Awake()
    {
        base.Awake();
        player = GetComponentInParent<Player>();
    }

    private void OnPlungeATK(Vector3 position)
    {

    }

    protected override void Update()
    {
        base.Update();
    }

    public override Vector3 GetCenterBound()
    {
        return MainCollider.bounds.center;
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        player.OnCollisionEnterEvent += OnCollisionEnterEvent;
        player.OnCollisionStayEvent += OnCollisionStayEvent;
        player.OnCollisionExitEvent += OnCollisionExitEvent;
        PlayerPlungeState.OnPlungeAction += OnPlungeATK;
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
    }

    protected override void Start()
    {
        base.Start();
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

    public override void KnockBack(Vector3 force)
    {
        player.Rb.AddForce(force, ForceMode.Impulse);
    }

    private void OnCollisionExitEvent(Collision collision)
    {
        if (characterStateMachine != null)
        {
            characterStateMachine.OnCollisionExit(collision);
        }
    }
}
