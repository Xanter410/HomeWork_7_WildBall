using UnityEngine;

[DisallowMultipleComponent]
public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _transformOfPlayer;
    [SerializeField] private float _turnRightLeftSpeed = 5f;
    [SerializeField] private float _turnUpDownSpeed = 2.0f;
    [SerializeField] private float _distance = -5f;
    [SerializeField] private float _height = 3f;

    private Vector3 _offset;

    private void Awake()
    {
        _transformOfPlayer = GameObject.FindWithTag("Player").transform;
    }

    private void Start()
    {
        CursorOff();
        _offset = new Vector3(0, _height, _distance);
    }

    private void LateUpdate()
    {
        _offset = Quaternion.AngleAxis(Input.GetAxis("Mouse Y") * _turnUpDownSpeed, Vector3.right) * _offset;
        _offset = Quaternion.AngleAxis(Input.GetAxis("Mouse X") * _turnRightLeftSpeed, Vector3.up) * _offset;

        var position = _transformOfPlayer.position;
        transform.position = position + _offset;
        transform.LookAt(position);
    }

    private static void CursorOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
