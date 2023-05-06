using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager Instance { get; private set; }

    public GameObject[] towerPrefabs;

    private int selectedTower = 0;

    public GameObject GetSelectedTower()
    {
        return towerPrefabs[selectedTower];
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
}
