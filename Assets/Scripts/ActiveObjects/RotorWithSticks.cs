using UnityEngine;

public class RotorWithSticks : Interactable
{
    [SerializeField] private float _speed = 100f;
    [SerializeField] private float _force = 200f;
    [SerializeField] private bool _isActive = true;
    private HingeJoint _hingeJoint;

    void Start()
    {
        _hingeJoint = GetComponentInChildren<HingeJoint>();

        JointMotor rotor;

        rotor = _hingeJoint.motor;

        rotor.targetVelocity = _speed;
        rotor.force = _force;
        _hingeJoint.motor = rotor;
        _hingeJoint.useMotor = _isActive;
    }

    public override void Activate(PlayerController player)
    {
        SwitchState();
    }

    private void SwitchState()
    {
        _isActive = _isActive ? false : true;
        _hingeJoint.useMotor = _isActive;
    }
}
