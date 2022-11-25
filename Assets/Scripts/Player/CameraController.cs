using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public Transform _transformOfPlayer;
    [SerializeField] private float _lookSpeed = 2.0f;
    [SerializeField] private float _minlookXLimit = -30.0f;
    [SerializeField] private float _maxLookXLimit = 59.0f;
    private Vector2 _rotation = Vector2.zero;

    private void Start()
    {
        CursorOff();
    }

    private void Update()
    {
        _rotation.y += UnityEngine.Input.GetAxis("Mouse X") * _lookSpeed;
        _rotation.x += -UnityEngine.Input.GetAxis("Mouse Y") * _lookSpeed;
        _rotation.x = Mathf.Clamp(_rotation.x, _minlookXLimit, _maxLookXLimit);
        transform.localRotation = Quaternion.Euler(_rotation.x, _rotation.y, 0);
        transform.position = _transformOfPlayer.position;
    }

    private static void CursorOff()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
