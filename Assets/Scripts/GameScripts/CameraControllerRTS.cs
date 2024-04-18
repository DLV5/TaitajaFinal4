using Cinemachine;
using UnityEngine;

public class CameraControllerRTS : MonoBehaviour
{
    [SerializeField] private float _cameraMoveSpeed = 20f;
    [SerializeField] private float _cameraZoomSpeed = 1f;

    [SerializeField] private float _minZoom = 20f;
    [SerializeField] private float _maxZoom = 5f;

    [SerializeField] private Transform _minPoint;
    [SerializeField] private Transform _maxPoint;

    [SerializeField] private CinemachineVirtualCamera _camera;

    private Vector3 _moveDirection = Vector3.zero;
    private float _zoomDirection = 0;

    private void Update()
    {
        _camera.transform.position += _moveDirection * _cameraMoveSpeed * Time.deltaTime;
        _camera.transform.position = new Vector3(
            Mathf.Clamp(_camera.transform.position.x, 
            _minPoint.transform.position.x + _camera.m_Lens.OrthographicSize * 2, 
            _maxPoint.transform.position.x - _camera.m_Lens.OrthographicSize * 2),
            Mathf.Clamp(_camera.transform.position.y, 
            _minPoint.transform.position.y + _camera.m_Lens.OrthographicSize, 
            _maxPoint.transform.position.y - _camera.m_Lens.OrthographicSize),
             _camera.transform.position.z
            );

        _camera.m_Lens.OrthographicSize += _zoomDirection * _cameraZoomSpeed * Time.deltaTime;
        _camera.m_Lens.OrthographicSize = Mathf.Clamp(_camera.m_Lens.OrthographicSize, _maxZoom, _minZoom);
    }

    public void MoveCamera(Vector3 moveTo) => _moveDirection = moveTo;

    public void Zoom(float direction)
    {
            _zoomDirection = -direction;
    }
}
