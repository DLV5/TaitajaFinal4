using UnityEngine;

public class UnitRTS : MonoBehaviour
{
    private GameObject _selectedGameObject;
    private IMovePosition _movePosition;

    private void Awake()
    {
        _selectedGameObject = transform.Find("Selected").gameObject;
        _movePosition = GetComponent<IMovePosition>();
        SetSelectedVisible(false);
    }

    public void SetSelectedVisible(bool visible) => _selectedGameObject.SetActive(visible);

    public void MoveTo(Vector3 targetPosition)
    {
        _movePosition.SetMovePosition(targetPosition);
    }
}
