using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;

    [Header("References")] 
    public Rigidbody2D rb;

    [Header("Attributes")] 
    private float projectileSpeed = 5f;

    private int damage = 1;
    
    private void FixedUpdate()
    {
        if (!target) return;
        
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * projectileSpeed;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // take health from enemy
        other.gameObject.GetComponent<Enemy>().TakeDamage(damage);
        Destroy(gameObject);
    }
}
