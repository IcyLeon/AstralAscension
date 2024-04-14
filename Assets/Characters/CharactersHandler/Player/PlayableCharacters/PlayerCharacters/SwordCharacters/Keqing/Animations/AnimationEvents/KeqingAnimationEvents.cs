using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeqingAnimationEvents : PlayableCharacterAnimationEvents
{
    [SerializeField] private GameObject HairpinTeleporterPrefab;
    private ObjectPool<HairpinTeleporter> objectPool;

    public Keqing keqing { 
        get 
        {
            return (Keqing)playableCharacters;
        }
    }

    private void Start()
    {
        objectPool = new ObjectPool<HairpinTeleporter>(HairpinTeleporterPrefab, playableCharacters.transform);
        objectPool.ObjectCreated += OnHairPinObjectCreated;
    }

    private void Update()
    {
        keqing.hairpinTeleporter = objectPool.GetPooledObject();
    }

    private void OnHairPinObjectCreated(GameObject existGO)
    {
        HairpinTeleporter HT = existGO.GetComponent<HairpinTeleporter>();
        if (HT == null)
            return;

        HT.SetPlayableCharacter(playableCharacters);
    }

    private void OnDestroy()
    {
        objectPool.ObjectCreated -= OnHairPinObjectCreated;
    }

    private void ShootTeleporter()
    {
    }
}
