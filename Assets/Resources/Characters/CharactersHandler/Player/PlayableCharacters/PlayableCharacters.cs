using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayableCharacters : DamageableCharacters, IPointOfInterest
{
    [field: SerializeField] public CapsuleCollider MainCollider { get; private set; }

    public Player player { get; private set; }

    public PlayableCharacterAnimationSO PlayableCharacterAnimationSO
    {
        get
        {
            return playerCharactersSO.PlayableCharacterAnimationSO;
        }
    }

    public PlayerCharactersSO playerCharactersSO
    {
        get
        {
            return damageableCharactersSO as PlayerCharactersSO;
        }
    }

    public PlayableCharacterStateMachine PlayableCharacterStateMachine
    {
        get
        {
            return characterStateMachine as PlayableCharacterStateMachine;
        }
    }

    public PlayableCharacterDataStat playableCharacterDataStat
    {
        get
        {
            return (PlayableCharacterDataStat)GetCharacterDataStat();
        }
    }

    public override CharacterDataStat GetCharacterDataStat()
    {
        return CharacterManager.instance.GetCharacterDataStat(CharacterSO);
    }

    protected override void Awake()
    {
        base.Awake();
        player = GetComponentInParent<Player>();
    }

    protected override void UpdateDataStat()
    {
    }

    public Transform GetIPointOfInterestTransform()
    {
        if (!Animator)
            return transform;

        return Animator.GetBoneTransform(HumanBodyBones.Head);
    }


    protected override void OnUpdate()
    {
        base.OnUpdate();
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
