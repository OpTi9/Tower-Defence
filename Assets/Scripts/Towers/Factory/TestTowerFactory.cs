using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTowerFactory : TowerFactory
{
    public GameObject testTowerPrefab;

    public override GameObject CreateTower(Vector3 position)
    {
        return GameObject.Instantiate(testTowerPrefab, position, Quaternion.identity);
    }
}
