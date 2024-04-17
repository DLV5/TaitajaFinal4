using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    //[SerializeField]
    //private int _interactionLayer;
    //private Camera mainCamera;

    //private Vector3 _lastPosition = Vector3.zero;
    //Ray ray;

    //private void Start()
    //{
    //    mainCamera = Camera.main;
    //    Debug.Log(LayerMask.LayerToName(_interactionLayer));
    //}

    //public Vector3 GetMousePositionOnMap()
    //{
    //    Vector3 mousePosition = Input.mousePosition;
    //    mousePosition.z = mainCamera.nearClipPlane;
    //    ray = mainCamera.ScreenPointToRay(mousePosition);
    //    RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction, 100, _interactionLayer);
    //    if (hit.collider != null)
    //    {
    //        _lastPosition = hit.point;
    //        Debug.Log("Aha! HIT! ");
    //    }
    //    return _lastPosition;
    //}
    //private void OnDrawGizmos()
    //{
    //    Gizmos.color = Color.green;
    //    Gizmos.DrawRay(ray);
    //}
}
