using UnityEngine;

public class AreaTrigger : MonoBehaviour
{
    [SerializeField] private Interactable _interactableObject;
    private PlayerController _controller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out _controller))
        {
            _controller.RegisterTriggerInteract(_interactableObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out _controller))
        {
            _controller.UnRegisterTriggerInteract(_interactableObject);
        }
    }
}
