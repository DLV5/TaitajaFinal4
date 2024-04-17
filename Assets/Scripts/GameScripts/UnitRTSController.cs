using System.Collections.Generic;
using UnityEngine;

public class UnitRTSController : MonoBehaviour
{
    private Vector3 _startPosition;

    private List<UnitRTS> _selectedUnitsRTS;

    private SelectionAreaUI _selectionAreaUI;
    private UnitsRTSPositionsCalculator _unitsRTSPositionsCalculator;

    private void Awake()
    {
        _selectedUnitsRTS = new List<UnitRTS>();
        _unitsRTSPositionsCalculator = new UnitsRTSPositionsCalculator();
        _selectionAreaUI = GameObject.Find("SelectionArea").GetComponent<SelectionAreaUI>();
        _selectionAreaUI.OnSelectionEnded();
    }

    public void OnSelectionStarted()
    {
        _startPosition = Utilities.GetMouseWorldPosition();

        _selectionAreaUI.OnSelectionStarted(_startPosition);
    }
    
    public void OnSelectionEnded() 
    {
        _selectionAreaUI.OnSelectionEnded();

        Collider2D[] collider2DArray = Physics2D.OverlapAreaAll(_startPosition, Utilities.GetMouseWorldPosition());

        DeselectAllUnits();

        _selectedUnitsRTS.Clear();

        SelectUnitsInCollider(collider2DArray);
    }

    public void MoveSelectedUnitsToMousePosition()
    {
        Vector3 moveToPosition = Utilities.GetMouseWorldPosition();

        List<Vector3> targetPositions =
            _unitsRTSPositionsCalculator.
            GetPositionListAround(moveToPosition, new float[] { 1.5f, 2.5f, 3.5f }, new int[] {5, 10, 20});

        int targetPositionIndex = 0;

        foreach(UnitRTS unitRTS in _selectedUnitsRTS)
        {
            unitRTS.MoveTo(targetPositions[targetPositionIndex]);
            targetPositionIndex = (targetPositionIndex + 1) % targetPositions.Count;
        }
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
}
