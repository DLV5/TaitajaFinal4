using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class GridSystem: MonoBehaviour
{
    private Tile _tilePrefab;

    private int[,] _gridArray;
    private Tile[,] _tileArray;

    private int _width;
    private int _height;
    private int _cellSize;

    public void CreateGrid(int width, int height, int cellSize, Tile tilePrefab)
    {
        _width = width;
        _height = height;
        _gridArray = new int[width, height];
        _tileArray = new Tile[width, height];
        _cellSize = cellSize;
        _tilePrefab = tilePrefab;

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

    public void SetValue(Vector3 worldPosition, Tile tile)
    {
        if (GetXY(worldPosition, out int x, out int y))
        {
            if(_tileArray[x, y] != null)
            {
                _tileArray[x, y] = null;
                SpawnObjectOnTile(tile.gameObject, GetWorldPosition(x, y));
            }
        }
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

    private Tile SpawnTile(Vector3 position)
    {
        Tile tile = Instantiate(_tilePrefab, position + new Vector3(_cellSize, _cellSize) * .5f, Quaternion.identity);
        return tile;
    }

    private void SpawnObjectOnTile(GameObject gameObject, Vector3 position)
    {
        GameObject newObject = Instantiate(gameObject, position + new Vector3(_cellSize, _cellSize) * .5f, Quaternion.identity);
    }
}
