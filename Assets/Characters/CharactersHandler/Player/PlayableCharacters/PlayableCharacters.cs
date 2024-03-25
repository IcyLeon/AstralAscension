using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacters : Characters
{
    [SerializeField] private PlayerCharactersSO PlayerCharactersSO;
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
