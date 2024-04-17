using UnityEngine;
using Pathfinding;

public class UnitRTS : MonoBehaviour
{
    private GameObject _selectedGameObject;
    private Seeker _seeker;

    private void Awake()
    {
        _selectedGameObject = transform.Find("Selected").gameObject;
        _seeker = GetComponent<Seeker>();
        SetSelectedVisible(false);
    }

    public void SetSelectedVisible(bool visible) => _selectedGameObject.SetActive(visible);

    public void MoveTo(Vector3 targetPosition)
    {
        _seeker.StartPath(transform.position, targetPosition);
    }
}
