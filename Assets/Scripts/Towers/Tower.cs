using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public abstract class Tower : MonoBehaviour
{
    public int damage;
    public float attackSpeed;
    private float rotationSpeed = 200f;
    public float range;

    public LayerMask enemyMask;

    private Transform target;
    public Transform rotationPoint;

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
            Debug.Log("no target");
        }
    }

    private void FindTarget()
    {
        Debug.Log("starting to look for target");
        RaycastHit2D[] hits =
            Physics2D.CircleCastAll(transform.position, range, (Vector2) transform.position, 0f, enemyMask);
        Debug.Log(hits);
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