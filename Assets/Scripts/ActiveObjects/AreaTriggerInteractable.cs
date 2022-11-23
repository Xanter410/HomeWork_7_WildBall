using UnityEngine;

public class AreaTriggerInteractable : MonoBehaviour
{
    [SerializeField] private Interactable _interactableObject;
    private PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out player))
        {
            player.RegisterInteractableObjectsCallback(_interactableObject.Activate);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out player))
        {
            player.UnRegisterInteractableObjectsCallback(_interactableObject.Activate);
        }
    }
}
