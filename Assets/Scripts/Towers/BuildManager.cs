using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }
    
    public TowerFactory[] towerFactories;
    public int[] towerCosts;
    private int selectedTower = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            for (int i = 0; i < towerFactories.Length; i++)
            {
                towerFactories[i].SetTowerCost(towerCosts[i]);
            }
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
            int towerCost = selectedTowerFactory.GetTowerCost();
            Debug.Log("Cost:" + towerCost);
            Debug.Log("Currency:" + CurrencyManager.Instance.currency);
            if (CurrencyManager.Instance.currency >= towerCost)
            {
                selectedTowerFactory.CreateTower(worldPosition);
                GridManager.Instance.grid[gridPosition.x, gridPosition.y] = 1; // Mark the cell as occupied
                CurrencyManager.Instance.SpendCurrency(towerCost); // substract tower's cost from player's currency
            }
            else
            {
                Debug.Log("Not enough currency to build this tower");
            }
        }
    }
}



