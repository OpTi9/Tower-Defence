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

    public GameObject BuildTower(Vector2Int gridPosition, Vector3 worldPosition)
    {
        GameObject builtTower = null;
        if (GameManager.Instance.currentState == GameManager.GameState.GameOver)
        {
            return null;
        }
    
        Plot plot = GridManager.Instance.GetPlotAtGridPosition(gridPosition);
        if (plot != null && !plot.HasTower()) // Check if the cell is empty
        {
            TowerFactory selectedTowerFactory = towerFactories[selectedTower];
            GameObject newTowerObject = selectedTowerFactory.CreateTower(worldPosition);
            Tower newTower = newTowerObject.GetComponent<Tower>();
        
            int towerCost = newTower.towerCost; // Get the cost from the tower itself
            Debug.Log("Cost:" + towerCost);
            Debug.Log("Currency:" + CurrencyManager.Instance.currency);
            
            if (CurrencyManager.Instance.currency >= towerCost)
            {
                plot.SetTower(newTowerObject);
                // Check if a path still exists
                List<Vector2Int> path = GameManager.Instance.GetPath();
                if (path == null)
                {
                    // If no path exists, revert the grid, destroy the tower, and notify the player
                    plot.RemoveTower();
                    Destroy(newTowerObject); // Destroy the tower if it blocks the path
                    Debug.Log("Cannot build tower. It would block the path.");
                    return null;
                }

                // If a path exists, build the tower and subtract its cost
                builtTower = newTowerObject;
                CurrencyManager.Instance.SpendCurrency(towerCost); // subtract tower's cost from player's currency
                builtTower.transform.SetParent(plot.transform);
            }
            else
            {
                Debug.Log("Not enough currency to build this tower");
                Destroy(newTowerObject); // Destroy the tower if not enough currency
            }
        }
    
        return builtTower;
    }


}



