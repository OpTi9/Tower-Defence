using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IceTower : Tower
{
    public float slowAmount = 0.5f; // Represents the percentage by which the speed is reduced (0.5 = 50% reduction)

    private void Update()
    {
        // Find all enemies within range
        RaycastHit2D[] hits =
            Physics2D.CircleCastAll(transform.position, range, (Vector2)transform.position, 0f, enemyMask);

        // Apply slow effect to enemies in range
        foreach (RaycastHit2D hit in hits)
        {
            Enemy enemy = hit.collider.GetComponent<Enemy>();
            if (enemy != null)
            {
                enemy.ApplySlow(slowAmount);
            }
        }
    }
}

