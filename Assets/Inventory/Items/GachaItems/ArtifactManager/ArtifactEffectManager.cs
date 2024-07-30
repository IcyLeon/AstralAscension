using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterArtifactManager
{
    private PlayableCharacterDataStat playableCharacterDataStat;
    private List<ArtifactEffect> artifactEffectList { get; }

    public CharacterArtifactManager(PlayableCharacterDataStat p)
    {
        playableCharacterDataStat = p;
        playableCharacterDataStat.OnItemEquippedChanged += PlayableCharacterDataStat_OnItemEquippedChanged;
        artifactEffectList = new();
    }

    private void PlayableCharacterDataStat_OnItemEquippedChanged(object sender, EventArgs e)
    {
    }

    public void OnDestroy()
    {
        playableCharacterDataStat.OnItemEquippedChanged -= PlayableCharacterDataStat_OnItemEquippedChanged;
    }
}
