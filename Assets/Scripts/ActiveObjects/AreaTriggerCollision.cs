using UnityEngine;

public class AreaTriggerCollision : MonoBehaviour
{
    [SerializeField] private Interactable _interactableObject;
    private PlayerController player;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<PlayerController>(out player))
        {
            _interactableObject.Activate(player);
        }
    }
}
