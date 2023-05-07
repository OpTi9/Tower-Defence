using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public float speed;

    private bool isDestroyed = false;

    public void Move(List<Vector2Int> path)
    {
        StartCoroutine(MoveAlongPath(path));
    }

    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0 && !isDestroyed)
        {
            // TODO: enemy spawner must be notified that this enemy is destroyed
            WaveBuilder.onEnemyDestroy.Invoke();
            isDestroyed = true;
            Destroy(gameObject);
        }
    }

    private IEnumerator MoveAlongPath(List<Vector2Int> path)
    {
        Vector3[] worldPath = new Vector3[path.Count];
        for (int i = 0; i < path.Count; i++)
        {
            worldPath[i] = GridManager.Instance.GetWorldPosition(path[i].x, path[i].y);
        }

        for (int i = 0; i < worldPath.Length; i++)
        {
            Vector3 target = worldPath[i];
            while (Vector3.Distance(transform.position, target) > 0.01f)
            {
                transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
                yield return null;
            }
        }

        // Enemy reached the end cell
        WaveBuilder.onEnemyDestroy.Invoke();
        Destroy(gameObject);
    }
}