using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTower : Tower
{
    void Start()
    {
        // Initialize NormalTower properties here
        damage = 10;
        attackSpeed = 1f;
        range = 3f;
    }

}