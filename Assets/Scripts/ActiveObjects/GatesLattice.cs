using UnityEngine;

public class GatesLattice : Interactable
{
    [SerializeField] private Vector3 _openOffset;
    [SerializeField] private float _speed = 3f;
    [SerializeField] private bool _IsOpen = false;
    private Rigidbody _rigidbody;
    private Vector3 _movePositionSpeed;
    private Vector3 _newPosition;
    private Vector3 _startPosition;
    private bool _isMove;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _startPosition = _rigidbody.position;
        _movePositionSpeed = _openOffset * Time.fixedDeltaTime * _speed;

        if (_IsOpen)
        {
            _rigidbody.position = _rigidbody.position + _openOffset;
        }
    }

    public override void Activate(PlayerController player)
    {
        if (_IsOpen)
        {
            _newPosition = _startPosition;
            _IsOpen = false;
        }
        else
        {
            _newPosition = _startPosition + _openOffset;
            _IsOpen = true;
        }
        _isMove = true;
    }

    private void FixedUpdate()
    {
        if (_isMove)
        {
            if (_IsOpen)
            {
                _rigidbody.MovePosition(_rigidbody.position + _movePositionSpeed);
            }
            else
            {
                _rigidbody.MovePosition(_rigidbody.position - _movePositionSpeed);
            }

            if (Vector3.Distance(_rigidbody.position, _newPosition) < 0.1f)
            {
                _isMove = false;
            }
        }
    }
}
