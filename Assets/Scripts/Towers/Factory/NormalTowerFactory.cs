using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTowerFactory : TowerFactory
{
    public GameObject normalTowerPrefab;

    public override GameObject CreateTower(Vector3 position)
    {
        return GameObject.Instantiate(normalTowerPrefab, position, Quaternion.identity);
    }
}

