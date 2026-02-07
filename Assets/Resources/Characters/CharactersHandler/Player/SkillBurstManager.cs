using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class SkillBurstManager : MonoBehaviour
{
    private static List<PlayerElementalState> PlayableCharacterState;

    private void Awake()
    {
        PlayableCharacterState = new();
    }

    public static void AddState(PlayerElementalState PlayerElementalState)
    {
        if (PlayableCharacterState.Contains(PlayerElementalState))
            return;

        PlayerElementalState.OnElementalStateEnter();
        PlayableCharacterState.Add(PlayerElementalState);
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateState();
    }

    private void UpdateState()
    {
        for(int i = 0; i < PlayableCharacterState.Count; i++)
        {
            PlayerElementalState state = PlayableCharacterState[i];
            if (state == null || state.IsElementalStateEnded())
            {
                if (state != null)
                {
                    state.OnElementalStateExit();
                }
                PlayableCharacterState.RemoveAt(i);
                return;
            }

            state.UpdateElementalState();
        }
    }
}
