using UnityEngine;

public class UnitRTS : MonoBehaviour
{
    private GameObject _selectedGameObject;

    private void Awake()
    {
        _selectedGameObject = transform.Find("Selected").gameObject;
        SetSelectedVisible(false);
    }

    public void SetSelectedVisible(bool visible) => _selectedGameObject.SetActive(visible);
}
