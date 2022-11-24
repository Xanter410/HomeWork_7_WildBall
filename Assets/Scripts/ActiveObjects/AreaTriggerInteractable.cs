using System.Collections.Generic;
using UnityEngine;

public class AreaTriggerInteractable : MonoBehaviour
{
    [SerializeField] private List<Interactable> _interactableObject;
    private PlayerController player;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out player))
        {
            foreach (var item in _interactableObject)
            {
                player.RegisterInteractableObjectsCallback(item.Activate);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent<PlayerController>(out player))
        {
            foreach (var item in _interactableObject)
            {
                player.UnRegisterInteractableObjectsCallback(item.Activate);
            }
        }
    }
}
