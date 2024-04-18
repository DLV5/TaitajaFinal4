using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class BuildFacilityButton : MonoBehaviour
{
    [SerializeField]
    private Facility _associatedFacility;
    private Button _button;
    private CustomBuildingCursor _buildingCursor;
    private void Start()
    {
        _buildingCursor = GameObject.Find("Building Manager").GetComponentInChildren<CustomBuildingCursor>();
        if(_buildingCursor == null)
        {
            Debug.LogWarning("_buildingCursor variable cannot be null! Ensure that the parent's name spelled right.");
        }
        _button= GetComponent<Button>();
        _button.onClick.AddListener(AssignCursorFacility);

        _associatedFacility.Initialize();
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveAllListeners();
    }

    private void AssignCursorFacility()
    {
        _buildingCursor.AssociatedFacility = _associatedFacility;
    }
}
