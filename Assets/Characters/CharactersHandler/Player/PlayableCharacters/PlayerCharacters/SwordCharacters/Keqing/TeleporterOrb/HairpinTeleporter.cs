using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HairpinTeleporter : MonoBehaviour
{
    private PlayableCharacters playableCharacters;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void OnDisable()
    {
        transform.SetParent(playableCharacters.transform);
    }

    public void SetPlayableCharacter(PlayableCharacters pc)
    {
        playableCharacters = pc;
    }
    private void OnTriggerEnter(Collider other)
    {
        
    }
}
