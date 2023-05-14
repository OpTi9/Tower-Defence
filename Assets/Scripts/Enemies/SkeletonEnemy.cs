using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonEnemy : Enemy
{
    protected override void Start()
    {
        base.Start();
        if(WaveManager.Instance.CurrentWave % 2 == 0)
        {
            health += 5;
        }
    }
}
