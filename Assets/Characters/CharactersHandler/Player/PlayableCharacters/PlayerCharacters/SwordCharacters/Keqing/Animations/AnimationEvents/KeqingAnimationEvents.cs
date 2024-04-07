using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAnimationEvents : PlayableCharacterAnimationEvents
{
    [SerializeField] private GameObject HairpinTeleporterPrefab;

    public Keqing keqing { 
        get 
        {
            return (Keqing)playableCharacters;
        }
    }

    private void Start()
    {
        InitHairPin(HairpinTeleporterPrefab);
    }

    private void InitHairPin(GameObject go)
    {
        GameObject hairpinGO = ObjectPoolManager.instance.CreateGameObject(go, playableCharacters.transform);
        HairpinTeleporter HT = hairpinGO.GetComponent<HairpinTeleporter>();
        HT.SetPlayableCharacter(keqing);
        hairpinGO.SetActive(false);
        keqing.hairpinTeleporter = HT;
    }

    private void ShootTeleporter()
    {

        keqing.hairpinTeleporter.gameObject.SetActive(true);
    }
}
