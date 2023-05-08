using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TowerFactory: MonoBehaviour
{
    protected int towerCost;

    public abstract GameObject CreateTower(Vector3 position);

    public int GetTowerCost()
    {
        return towerCost;
    }

    public void SetTowerCost(int cost)
    {
        towerCost = cost;
    }

}

