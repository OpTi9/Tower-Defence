using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTowerFactory : TowerFactory
{
    public GameObject fireTowerPrefab;

    public override GameObject CreateTower(Vector3 position)
    {
        return GameObject.Instantiate(fireTowerPrefab, position, Quaternion.identity);
    }
}
