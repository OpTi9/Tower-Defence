using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public int damage;
    public float attackSpeed;
    public float range;
    public int cost;

    public GameObject projectilePrefab;
    public Transform firingPoint;
    
    public LayerMask enemyMask;
    
    private float rotationSpeed = 200f;
    public Transform rotationPoint;
    
    private Transform target;
    private float timeUntilFire;

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
            return;
        }

        RotateTowardsTarget();

        if (!CheckTargetIsInRange())
        {
            target = null;
        }
        else
        {
            // shoot
            timeUntilFire += Time.deltaTime;

            if (timeUntilFire >= 1f / attackSpeed)
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        GameObject projectileObject = Instantiate(projectilePrefab, firingPoint.position, Quaternion.identity);
        Projectile projectileScript = projectileObject.GetComponent<Projectile>();
        projectileScript.SetTarget(target);
        
        timeUntilFire = 0f;
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits =
            Physics2D.CircleCastAll(transform.position, range, (Vector2) transform.position, 0f, enemyMask);
        
        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }
    }

    private void RotateTowardsTarget()
    {
        float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) *
                      Mathf.Rad2Deg + 180f;

        Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
        rotationPoint.rotation = Quaternion.RotateTowards(rotationPoint.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= range;
    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, range);
    }
}