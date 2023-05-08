using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    public int health;
    public float speed;
    public int reward;

    private bool isDestroyed = false;
    
    private float originalSpeed;
    private Coroutine slowRoutine;
    
    public SpriteRenderer spriteRenderer;
    
    private void Start()
    {
        originalSpeed = speed;
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    public void Move(List<Vector2Int> path)
    {
        StartCoroutine(MoveAlongPath(path));
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        StartCoroutine(DamageBlink());

        if (health <= 0 && !isDestroyed)
        {
            WaveManager.onEnemyDestroy.Invoke();
            isDestroyed = true;
            Destroy(gameObject);
            CurrencyManager.Instance.IncreaseCurrency(reward);
        }
    }
    
    public void ApplySlow(float slowAmount)
    {
        if (slowRoutine != null)
        {
            StopCoroutine(slowRoutine);
        }
        slowRoutine = StartCoroutine(SlowRoutine(slowAmount));
    }
    
    private IEnumerator DamageBlink()
    {
        Color originalColor = spriteRenderer.color;

        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(0.1f); // Adjust the blink duration here

        spriteRenderer.color = originalColor;
    }

    private IEnumerator SlowRoutine(float slowAmount)
    {
        // Set color to light blue
        spriteRenderer.color = new Color(0.6f, 0.8f, 1f, 1f);

        speed = originalSpeed * (1f - slowAmount);
        yield return new WaitForSeconds(1f); // The slow duration can be adjusted here
        speed = originalSpeed;

        // Reset color to the original color
        spriteRenderer.color = Color.white;

        slowRoutine = null;
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
        WaveManager.onEnemyDestroy.Invoke();
        Destroy(gameObject);
    }
}