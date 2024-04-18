using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GridSystem _gridSystem;
    [SerializeField] private CustomBuildingCursor _buildingCursor;

    [Header("Grid parameters")]
    [SerializeField] private Tile _tilePrefab;
    //[SerializeField] private Tile _selectedTilePrefab;
    [SerializeField] private Transform _gridTilesParent;
    [SerializeField] private int _gridWidth;
    [SerializeField] private int _gridHeight;
    [SerializeField] private int _gridCellSize;

    public bool IsBuildingMode { get; private set; }

    private void Start()
    {
        _gridSystem.CreateGrid(_gridWidth, _gridHeight, _gridCellSize, _tilePrefab, _gridTilesParent);
        _buildingCursor.Initialize();
    }
    private void Update()
    {
        if (!IsBuildingMode)
            return;

        if (_buildingCursor.AssociatedFacility == null)
            return;

        if (_gridSystem.IsPlacementAvailable(Camera.main.ScreenToWorldPoint(Input.mousePosition), _buildingCursor.AssociatedFacility))
            _buildingCursor.HighlightPermission();
        else
            _buildingCursor.HighlightProhibition();

        if(Input.GetMouseButtonDown(0))
        {
            _gridSystem.SetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition), _buildingCursor.AssociatedFacility);
            _buildingCursor.ResetCursor();
        }
    }
    public void EnterBuildingMode()
    {
        if (IsBuildingMode)
            return;

        IsBuildingMode = true;
        _gridTilesParent.gameObject.SetActive(true);
    }
    public void ExitBuildingMode()
    {   
        if(!IsBuildingMode)
            return;

        IsBuildingMode = false;
        _gridTilesParent.gameObject.SetActive(false);
        _buildingCursor.ResetCursor();
    }
}
