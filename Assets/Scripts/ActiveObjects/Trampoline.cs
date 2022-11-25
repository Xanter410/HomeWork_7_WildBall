using UnityEngine;

public class Trampoline : Interactable
{
    [SerializeField] private int _power;

    public override void Activate(PlayerController player)
    {
        Vector3 hitVector = Vector3.down * _power;

        Rigidbody rb = player.GetComponent<Rigidbody>();

        rb.AddForce(-hitVector, ForceMode.Impulse);
    }
}
