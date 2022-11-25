using UnityEngine;
public interface IInteractable
{
    public void Activate(PlayerController player);
}

public abstract class Interactable : MonoBehaviour, IInteractable
{
    public abstract void Activate(PlayerController player);
}
