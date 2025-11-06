using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public abstract class PlayableCharacters : DamageableCharacters, IPointOfInterest
{
    [field: SerializeField] public CapsuleCollider MainCollider { get; private set; }
    private InteractSensor interactSensor;
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

    public PlayableCharacterStateMachine playableCharacterStateMachine { get; private set; }

    protected override CharacterStateMachine CreateStateMachine()
    {
        if (playableCharacterStateMachine == null)
        {
            playableCharacterStateMachine = CreatePlayableCharacterStateMachine();
        }
        return playableCharacterStateMachine;
    }

    protected abstract PlayableCharacterStateMachine CreatePlayableCharacterStateMachine();

    public PlayableCharacterDataStat playableCharacterDataStat
    {
        get
        {
            return (PlayableCharacterDataStat)GetCharacterDataStat();
        }
    }

    private void CreateInteraction()
    {
        interactSensor = gameObject.AddComponent<PlayerInteractSensor>();
        interactSensor.CreateCollider(MainCollider.height, MainCollider.center);

    }

    protected override void Awake()
    {
        base.Awake();
        player = GetComponentInParent<Player>();
        CreateInteraction();
    }

    protected override void OnAnimationMove(Vector3 deltaPosition)
    {
        base.OnAnimationMove(deltaPosition);
        
        player.transform.position += deltaPosition;
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
    }

    protected override void Start()
    {
        base.Start();
    }

    public override void KnockBack(Vector3 force)
    {
        player.Rb.AddForce(force, ForceMode.Impulse);
    }
}
