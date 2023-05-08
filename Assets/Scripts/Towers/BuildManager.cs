using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }
    
    public TowerFactory[] towerFactories;
    private int selectedTower = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetSelectedTower(int selectedTower)
    {
        this.selectedTower = selectedTower;
    }

    public void BuildTower(Vector2Int gridPosition, Vector3 worldPosition)
    {
        if (GridManager.Instance.grid[gridPosition.x, gridPosition.y] == 0) // Check if the cell is empty
        {
            TowerFactory selectedTowerFactory = towerFactories[selectedTower];
            selectedTowerFactory.CreateTower(worldPosition);
            GridManager.Instance.grid[gridPosition.x, gridPosition.y] = 1; // Mark the cell as occupied
        }
    }
}



