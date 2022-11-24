using UnityEngine;

public class MobilePlatform : Interactable
{
    [SerializeField] private bool _setActive = false;
    [SerializeField] private float _speed = 1;
    [SerializeField] private Transform point_1;
    [SerializeField] private Transform point_2;
    private Rigidbody _rigidbody;
    private Vector3 _movePoint1Speed;
    private Vector3 _movePoint2Speed;
    private bool _direction = true;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _movePoint1Speed = (_rigidbody.position - point_1.position).normalized * Time.fixedDeltaTime * _speed;
        _movePoint2Speed = (_rigidbody.position - point_2.position).normalized * Time.fixedDeltaTime * _speed;

        gameObject.SetActive(_setActive);
    }
    private void FixedUpdate()
    {
        if (_direction)
        {
            _rigidbody.MovePosition(_rigidbody.position - _movePoint1Speed);

            if (Vector3.Distance(_rigidbody.position, point_1.position) <= 0.3f)
            {
                _direction = false;
            }
        }
        else
        {
            _rigidbody.MovePosition(_rigidbody.position - _movePoint2Speed);

            if (Vector3.Distance(_rigidbody.position, point_2.position) <= 0.3f)
            {
                _direction = true;
            }
        }
    }

    public override void Activate(PlayerController player)
    {
        SwitchState();
    }

    private void SwitchState()
    {
        _setActive = _setActive ? false : true;

        gameObject.SetActive(_setActive);
    }

}
