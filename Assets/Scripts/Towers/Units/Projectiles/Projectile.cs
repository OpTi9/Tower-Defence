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

    protected int damage;
    
    private void FixedUpdate()
    {
        if (!target) return;
        
        Vector2 direction = (target.position - transform.position).normalized;

        rb.velocity = direction * projectileSpeed;
    }
    
    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    protected virtual void OnHitEnemy(Enemy enemy)
    {
        enemy.TakeDamage(damage);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        OnHitEnemy(enemy);
        Destroy(gameObject);
    }
}
