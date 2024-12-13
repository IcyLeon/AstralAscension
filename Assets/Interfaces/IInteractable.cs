using UnityEngine;

public interface IInteractable : IPointOfInterest
{
    void Interact(Player player);
}
