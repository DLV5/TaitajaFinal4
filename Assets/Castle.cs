using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    private BuildingManager _buildingManager;
    [SerializeField]
    private float _buildingArea;

    [Header("Building UI")]
    [SerializeField] private GameObject _buildingPanel;

    public event Action OnCastleClick;
    private void Start()
    {
        _buildingManager = GameObject.Find("Building Manager").GetComponent<BuildingManager>();
        ExitBuildingMode();
    }
    private void OnMouseDown()
    {
        if (_buildingManager.IsBuildingMode)
            ExitBuildingMode();
        else
            EnterBuildingMode();
    }
    private void Update()
    {
        if (!_buildingManager.IsBuildingMode)
            return;
        if (Vector3.Distance(transform.position, Input.mousePosition) >= _buildingArea)
            ExitBuildingMode();
    }
    public void EnterBuildingMode()
    {
        _buildingManager.EnterBuildingMode();
        _buildingPanel.SetActive(true);
    }
    public void ExitBuildingMode()
    {
        _buildingManager.ExitBuildingMode();
        _buildingPanel.SetActive(false);
    }

}
