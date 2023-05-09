using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class EnemyType1 : Enemy
{
    protected override void Start()
    {
        base.Start();
        Debug.Log("enemyType1");
    }
}
