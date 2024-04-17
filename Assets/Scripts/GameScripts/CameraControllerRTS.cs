using Cinemachine;
using UnityEngine;

public class CameraControllerRTS : MonoBehaviour
{
    [SerializeField] private float _cameraMoveSpeed = 20f;
    [SerializeField] private float _cameraZoomSpeed = 2f;
    [SerializeField] private float _minZoom = 20f;
    [SerializeField] private float _maxZoom = 5f;

    [SerializeField] private CinemachineVirtualCamera _camera;

    private Vector3 _moveDirection = Vector3.zero;
    private float _zoomDirection = 0;

    private void Update()
    {
        _camera.transform.position += _moveDirection * _cameraMoveSpeed * Time.deltaTime;
        _camera.m_Lens.OrthographicSize += _zoomDirection * _cameraZoomSpeed * Time.deltaTime;
    }

    public void MoveCamera(Vector3 moveTo) => _moveDirection = moveTo;

    public void Zoom(float direction)
    {
        if (!IsCameraInBounds())
        {
            _zoomDirection = 0;
            return;
        }
            _zoomDirection = -direction;
    }

    private bool IsCameraInBounds()
    {
        return _camera.m_Lens.OrthographicSize >= _maxZoom && _camera.m_Lens.OrthographicSize <= _minZoom;
    }
}
