using System.Collections.Generic;
using UnityEngine;

public class AreaTriggerCollision : MonoBehaviour
{
    [SerializeField] private List<Interactable> _interactableObject;
    private PlayerController player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out player))
        {
            foreach (var item in _interactableObject)
            {
                item.Activate(player);
            }
        }
    }
}
