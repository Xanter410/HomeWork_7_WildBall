using UnityEngine;
public interface IInteractable
{
    public void Activate();
}

public abstract class Interactable : MonoBehaviour, IInteractable
{
    public abstract void Activate();
}
