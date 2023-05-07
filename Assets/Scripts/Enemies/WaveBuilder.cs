using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WaveBuilder : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private GameObject[] enemyPrefabs;

    [Header("Wave Settings")]
    [SerializeField] private int baseEnemies = 8;
    [SerializeField] private float enemiesPerSecond = 0.5f;
    [SerializeField] private float timeBetweenWaves = 5f;
    [SerializeField] private float difficultyScalingFactor = 0.75f;

    public static UnityEvent onEnemyDestroy = new UnityEvent();

    private int currentWave = 1;
    private float timeSinceLastSpawn;
    private int enemiesAlive;
    private int enemiesLeftToSpawn;
    private bool isSpawning = false;
    private List<Vector2Int> currentPath;

    private void Awake()
    {
        onEnemyDestroy.AddListener(EnemyDestroyed);
    }

    public void StartSpawningEnemies(List<Vector2Int> path)
    {
        currentPath = path;
        StartCoroutine(StartWave());
    }

    private void EnemyDestroyed()
    {
        enemiesAlive--;
    }
    
    private IEnumerator StartWave(float delay = 0f)
    {
        Debug.Log("wave started");
        yield return new WaitForSeconds(delay);
        GameManager.Instance.ChangeState(GameManager.GameState.Wave);
        isSpawning = true;
        enemiesLeftToSpawn = EnemiesPerWave();
    }

    private void EndWave()
    {
        Debug.Log("wave ended");
        GameManager.Instance.ChangeState(GameManager.GameState.Building);
        isSpawning = false;
        timeSinceLastSpawn = 0f;
        currentWave++;
        StartCoroutine(StartWave(timeBetweenWaves));
    }

    private void Update()
    {
        if (!isSpawning) return;

        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn >= (1f / enemiesPerSecond) && enemiesLeftToSpawn > 0)
        {
            SpawnEnemy();
            enemiesLeftToSpawn--;
            enemiesAlive++;
            timeSinceLastSpawn = 0f;
        }

        if (enemiesAlive == 0 && enemiesLeftToSpawn == 0)
        {
            EndWave();
        }
    }

    private void SpawnEnemy()
    {
        GameObject prefabToSpawn = enemyPrefabs[0];
        Enemy newEnemy = Instantiate(prefabToSpawn).GetComponent<Enemy>();
        newEnemy.Move(currentPath);
    }

    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
