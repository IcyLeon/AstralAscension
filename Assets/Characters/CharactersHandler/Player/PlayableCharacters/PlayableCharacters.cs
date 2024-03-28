using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacters : Characters
{
    [SerializeField] private PlayerCharactersSO PlayerCharactersSO;

    [field: SerializeField] public CapsuleCollider MainCollider { get; private set; }

    public Player player { get; private set; }

    protected override void Start()
    {
        player = transform.parent.GetComponent<Player>();
        base.Start();
    }

    protected override void Update()
    {
        base.Update();
    }

}
