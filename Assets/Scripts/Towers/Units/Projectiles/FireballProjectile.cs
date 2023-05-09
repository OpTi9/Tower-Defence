using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IFireDamageReceiver
{
    void ReceiveFireDamage(int fireDamage);
}


public class FireballProjectile : Projectile
{
    
    protected override void OnHitEnemy(Enemy enemy)
    {
        IFireDamageReceiver fireDamageReceiver = enemy as IFireDamageReceiver;
        if (fireDamageReceiver != null)
        {
            fireDamageReceiver.ReceiveFireDamage(damage * 2); // Deal double damage
        }
        else
        {
            base.OnHitEnemy(enemy);
        }
    }
}
