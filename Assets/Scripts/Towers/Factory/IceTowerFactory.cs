using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTowerFactory : TowerFactory
{
    public GameObject iceTowerPrefab;

    public override GameObject CreateTower(Vector3 position)
    {
        return GameObject.Instantiate(iceTowerPrefab, position, Quaternion.identity);
    }
}
