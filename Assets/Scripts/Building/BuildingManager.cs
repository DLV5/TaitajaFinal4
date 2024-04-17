using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    [SerializeField] private GridSystem _gridSystem;

    [SerializeField] private Tile _tilePrefab;
    [SerializeField] private Tile _selectedTilePrefab;

    [SerializeField] private int _gridWidth;
    [SerializeField] private int _gridHeight;
    [SerializeField] private int _gridCellSize;

    private void Start()
    {
        _gridSystem.CreateGrid(_gridWidth, _gridHeight, _gridCellSize, _tilePrefab);
    }
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            _gridSystem.SetValue(Camera.main.ScreenToWorldPoint(Input.mousePosition), _selectedTilePrefab);
        }
    }
}
