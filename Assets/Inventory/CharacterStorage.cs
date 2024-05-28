using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CharacterStorage
{
    public Dictionary<PlayerCharactersSO, CharacterDataStat> playableCharacterStatList { get; }
    public Dictionary<PlayerCharactersSO, CharacterDataStat> equippedPlayableCharacterStatList { get; }

    public void Update()
    {
        for (int i = 0; i < playableCharacterStatList.Count; i++)
        {
            CharacterDataStat cd = playableCharacterStatList.ElementAt(i).Value;
            cd.Update();
        }
    }

    public CharacterStorage()
    {
        playableCharacterStatList = equippedPlayableCharacterStatList = new();
    }
}
