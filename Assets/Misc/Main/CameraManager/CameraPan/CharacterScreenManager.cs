using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterScreenManager : MonoBehaviour
{
    [SerializeField] private CharacterSelection characterSelection;
    public static event Action<CharactersSO> OnCharacterSelected;

    private void Awake()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
