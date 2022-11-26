using UnityEngine;

[DisallowMultipleComponent]
public class CameraCollision : MonoBehaviour
{
    [SerializeField] private Transform _transformOfPlayer;
    [SerializeField] private Transform _referenceTransform;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private float _collisionOffset = 0.3f;
    [SerializeField] private float _cameraSpeed = 15f; 
    private Vector3 _defaultPos;
    private Vector3 _directionNormalized;
    private Transform _parentTransform;
    private float _defaultDistance;

    void Start()
    {
        _defaultPos = transform.localPosition;
        _directionNormalized = _defaultPos.normalized;
        _parentTransform = transform.parent;
        _defaultDistance = Vector3.Distance(_defaultPos, Vector3.zero);
    }

    void LateUpdate()
    {
        Vector3 currentPos = _defaultPos;
        RaycastHit hit;
        Vector3 dirTmp = _parentTransform.TransformPoint(_defaultPos) - _referenceTransform.position;
        if (Physics.SphereCast(_referenceTransform.position, _collisionOffset, dirTmp, out hit, _defaultDistance, _layerMask))
        {
            currentPos = (_directionNormalized * (hit.distance - _collisionOffset));

            transform.localPosition = currentPos;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, currentPos, Time.deltaTime * _cameraSpeed);
        }
        transform.LookAt(_transformOfPlayer.position);
    }
}
