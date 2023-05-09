using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTower : Tower
{
    
    protected override void Shoot()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
        FireballProjectile fireballProjectile = projectileObject.GetComponent<FireballProjectile>();
        fireballProjectile.SetTarget(target);
        fireballProjectile.SetDamage(damage);
        timeUntilFire = 0f;
    }
}