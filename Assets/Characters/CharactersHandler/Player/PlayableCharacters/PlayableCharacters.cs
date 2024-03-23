using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayableCharacters : Characters
{
    [SerializeField] private PlayerCharactersSO PlayerCharactersSO;

    protected override void Start()
    {
        base.Start();

    }
}
