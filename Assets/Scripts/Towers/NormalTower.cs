using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalTower : Tower
{
    void Start()
    {
        // Initialize NormalTower properties here
        damage = 0;
        attackSpeed = 2f;
        range = 2f;
    }

}