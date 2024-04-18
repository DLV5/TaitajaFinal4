using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSystem: MonoBehaviour
{
    private Transform _tilesParent;
    private Tile _tilePrefab;

    private int[,] _gridArray;
    private Tile[,] _tileArray;

    private int _width;
    private int _height;
    private int _cellSize;

    public void CreateGrid(int width, int height, int cellSize, Tile tilePrefab, Transform tilesParent)
    {
        _width = width;
        _height = height;
        _tileArray = new Tile[width, height];
        _cellSize = cellSize;
        _tilePrefab = tilePrefab;
        _tilesParent = tilesParent;

        //Instantiating the grid
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                _tileArray[x, y] = SpawnTile(GetWorldPosition(x, y));
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x + _cellSize, y), Color.red, 100f);
                Debug.DrawLine(GetWorldPosition(x, y), GetWorldPosition(x, y + _cellSize), Color.red, 100f);
            }
        }
        Debug.Log(_tileArray.GetLength(0) +" " + _tileArray.GetLength(1));
    }

    public void SetValue(Vector3 worldPosition, Facility facility)
    {
        if (GetXY(worldPosition, out int x, out int y))
        {
            List<CellPosition> requiredCells = CalculateTakenCells(x, y, facility.HeightInCells, facility.WidthInCells);
            if (IsPlacementAvailable(requiredCells))
            {
                foreach (CellPosition CellPosition in requiredCells)
                {
                    _tileArray[CellPosition.x, CellPosition.y] = null;
                }
                SpawnFacilityOnTile(facility, GetWorldPosition(x, y));
                Debug.Log("New House Placed :)");
            }
        }
    }

    /// <summary>
    /// Returns true if the object can be placed at the world given position, otherwise false.
    /// </summary>
    /// <param name="worldPosition"></param>
    /// <param name="facility"></param>
    /// <returns></returns>
    public bool IsPlacementAvailable(Vector3 worldPosition, Facility facility)
    {
        if (GetXY(worldPosition, out int x, out int y))
        {
            List<CellPosition> requiredCells = CalculateTakenCells(x, y, facility.HeightInCells, facility.WidthInCells);
            if (IsPlacementAvailable(requiredCells))
                return true;
        }
        return false;
    }

    private Vector3 GetWorldPosition(int x, int y)
    {
        return new Vector3(x, y) * _cellSize;
    }

    private bool GetXY(Vector3 worldPosition, out int x, out int y)
    {
        int testX = Mathf.FloorToInt(worldPosition.x/ _cellSize);
        int testY = Mathf.FloorToInt(worldPosition.y/ _cellSize);
        if(testX >= 0 && testY >= 0 && testX < _width && testY < _height)  // Checking if the world position intersecting the grid 
        {
            x = testX;
            y = testY;
            return true;
        }
        x = 0;
        y = 0;
        return false;
    }

    /// <summary>
    /// Calculates how many cells the object will occupy and returns a list of the positions of those cells
    /// </summary>
    /// <param name="x"></param>
    /// <param name="y"></param>
    /// <param name="requiredHeight"></param>
    /// <param name="requiredWidth"></param>
    /// <returns></returns>
    private List<CellPosition> CalculateTakenCells(int x, int y, int requiredHeight, int requiredWidth)
    {
        List<CellPosition> cells = new List<CellPosition>();
        for (int i = x; i < requiredWidth + x; i++)
        {
            for (int j = y; j < requiredHeight + y; j++)
            {
                CellPosition newPos = new CellPosition(i, j);
                cells.Add(newPos);
            }
        }
        return cells;
    }

    /// <summary>
    /// Returns true if the object can be placed at the desirable position, otherwise false. Based on the x and y cell indices.
    /// </summary>
    /// <param name="requiredCells"></param>
    /// <returns></returns>
    private bool IsPlacementAvailable(List<CellPosition> requiredCells)
    {
        int freeTilesAmount = 0;

        foreach (CellPosition CellPosition in requiredCells)        
        {
            if (_tileArray[CellPosition.x, CellPosition.y] != null)
                freeTilesAmount++;
        }
        if (freeTilesAmount != requiredCells.Count)
            return false;

        return true;
    }

    private Tile SpawnTile(Vector3 position)
    {
        Tile tile = Instantiate(_tilePrefab, position + new Vector3(_cellSize, _cellSize) * .5f, Quaternion.identity, _tilesParent);
        return tile;
    }

    private void SpawnFacilityOnTile(Facility facility, Vector3 position)
    {
        Facility newObject = Instantiate(facility, position + new Vector3(_cellSize, _cellSize) * .5f, Quaternion.identity);
        newObject.StartActivity();
    }
    private struct CellPosition
    {
        public CellPosition(int X, int Y)
        {
            x = X;
            y = Y;   
        }
        public int x; 
        public int y;
    }
}
