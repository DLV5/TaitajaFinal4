using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GridSystem _gridSystem;
    [SerializeField] private CustomBuildingCursor _buildingCursor;

    [Header("UI")]
    [SerializeField] private GameObject _buildingPanel;

    [Header("Grid parameters")]
    [SerializeField] private Tile _tilePrefab;
    //[SerializeField] private Tile _selectedTilePrefab;

    [SerializeField] private int _gridWidth;
    [SerializeField] private int _gridHeight;
    [SerializeField] private int _gridCellSize;

    private Vector3 _mousePosition;

    private void Start()
    {
        _gridSystem.CreateGrid(_gridWidth, _gridHeight, _gridCellSize, _tilePrefab);
        _buildingCursor.Initialize();
        _buildingPanel.SetActive(false);
    }
    private void Update()
    {
        if (_buildingCursor.AssociatedFacility == null)
            return;

        if (_gridSystem.IsPlacementAvailable(Camera.main.ScreenToWorldPoint(Input.mousePosition), _buildingCursor.AssociatedFacility))
        {
            _buildingCursor.HighlightPermission();
        }
        else
        {
            _buildingCursor.HighlightProhibition();
        }

        if(Input.GetMouseButtonDown(0))
        {
            _gridSystem.SetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition), _buildingCursor.AssociatedFacility);
            _buildingCursor.OnFacilityBuilded();
        }

    }
}
