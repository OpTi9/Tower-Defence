using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireVulnerableEnemy : Enemy, IFireDamageReceiver
{
    public float fireDamageMultiplier = 2f;

    protected override void Start()
    {
        base.Start();
        if(WaveManager.Instance.CurrentWave % 3 == 0)
        {
            health += 5;
        }
    }

    public void ReceiveFireDamage(int fireDamage)
    {
        int increasedDamage = Mathf.RoundToInt(fireDamage * fireDamageMultiplier);
        TakeDamage(increasedDamage);
    }
}


