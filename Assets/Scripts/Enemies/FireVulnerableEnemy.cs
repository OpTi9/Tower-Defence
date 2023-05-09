using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireVulnerableEnemy : Enemy, IFireDamageReceiver
{
    public float fireDamageMultiplier = 2f;

    public void ReceiveFireDamage(int fireDamage)
    {
        int increasedDamage = Mathf.RoundToInt(fireDamage * fireDamageMultiplier);
        TakeDamage(increasedDamage);
    }
}
