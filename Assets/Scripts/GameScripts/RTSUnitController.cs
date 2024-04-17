using System.Collections.Generic;
using UnityEngine;

public class RTSUnitController : MonoBehaviour
{
    [SerializeField] private Transform _selectionAreaTransform;
    private bool _shouldScaleArea = false;

    private Vector3 _startPosition;

    private List<UnitRTS> _selectedUnitsRTS;

    private void Awake()
    {
        _selectedUnitsRTS = new List<UnitRTS>();
        _selectionAreaTransform.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (_shouldScaleArea)
            ScaleSelectionArea();
    }

    public void OnSelectionStarted()
    {
        _selectionAreaTransform.gameObject.SetActive(true);
        _shouldScaleArea = true;

        _startPosition = GetMouseWorldPosition();
    }
    
    public void OnSelectionEnded() 
    {
        _selectionAreaTransform.gameObject.SetActive(false);
        _shouldScaleArea = false;

        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(_startPosition, GetMouseWorldPosition());

        DeselectAllUnits();

        _selectedUnitsRTS.Clear();

        SelectUnitsInCollider(collider2DArray);
    }

    private void ScaleSelectionArea()
    {
        Vector3 currentMousePosition = GetMouseWorldPosition();

        Vector3 lowerLeft = new Vector3(
            Mathf.Min(_startPosition.x, currentMousePosition.x),
            Mathf.Min(_startPosition.y, currentMousePosition.y)
            );
        
        Vector3 upperRight = new Vector3(
            Mathf.Max(_startPosition.x, currentMousePosition.x),
            Mathf.Max(_startPosition.y, currentMousePosition.y)
            );

        _selectionAreaTransform.position = lowerLeft;
        _selectionAreaTransform.localScale =  upperRight - lowerLeft;
    }

    private void DeselectAllUnits()
    {
        foreach (var unitRTS in _selectedUnitsRTS)
        {
            unitRTS.SetSelectedVisible(false);
        }
    }

    private void SelectUnitsInCollider(Collider2D[] collider2DArray)
    {
        foreach (var collider2D in collider2DArray)
        {
            UnitRTS unitRTS = collider2D.GetComponent<UnitRTS>();

            if (unitRTS != null)
            {
                unitRTS.SetSelectedVisible(true);
                _selectedUnitsRTS.Add(unitRTS);
            }
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPosition.z = 0;

        return worldPosition;
    }
}
