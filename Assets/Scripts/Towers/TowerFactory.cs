using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    public GameObject normalTowerPrefab;

    public Tower CreateNormalTower(Vector3 position)
    {
        GameObject towerInstance = Instantiate(normalTowerPrefab, position, Quaternion.identity);
        return towerInstance.GetComponent<NormalTower>();
    }
}
