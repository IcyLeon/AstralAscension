using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestInteract : MonoBehaviour, IInteractable, IPointOfInterest
{
    public Transform GetIPointOfInterestTransform()
    {
        return transform;
    }

    public void Interact(Player Player)
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
