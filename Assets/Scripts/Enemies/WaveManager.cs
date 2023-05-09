using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class WaveManager : MonoBehaviour
{
    [Header("Attributes")]
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] TextMeshProUGUI waveUI;

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

    public void StartSpawningEnemies()
    {
        StartCoroutine(StartWave());
    }
    
    private void OnGUI()
    {
        waveUI.text = currentWave.ToString();
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

        // Update the currentPath before starting a new wave
        currentPath = GameManager.Instance.GetPath();

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
        if (GameManager.Instance.currentState == GameManager.GameState.GameOver)
        {
            return;
        }
        
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
        int enemyIndex = GetEnemyIndexForWave();
        GameObject prefabToSpawn = enemyPrefabs[enemyIndex];
        GameObject newEnemyObject = Instantiate(prefabToSpawn);

        // Set the enemy's position to the start cell's world position
        Vector3 startPosition = GridManager.Instance.GetWorldPosition(currentPath[0].x, currentPath[0].y);
        newEnemyObject.transform.position = startPosition;

        Enemy newEnemy = newEnemyObject.GetComponent<Enemy>();
        newEnemy.Move(currentPath);
    }
    
    private int GetEnemyIndexForWave()
    {
        int enemyPrefabsCount = enemyPrefabs.Length;
        int[] enemyWeights = new int[enemyPrefabsCount];

        // Calculate weights based on the current wave and enemy prefabs count
        for (int i = 0; i < enemyPrefabsCount; i++)
        {
            enemyWeights[i] = Mathf.Max(1, currentWave - i);
        }

        // Calculate the total weight
        int totalWeight = enemyWeights.Sum();

        // Generate a random number between 0 and total weight
        int randomNumber = UnityEngine.Random.Range(0, totalWeight);

        // Go through enemy prefabs and check if the random number falls within the range
        int rangeMin = 0;
        for (int i = 0; i < enemyPrefabsCount; i++)
        {
            int rangeMax = rangeMin + enemyWeights[i];
            if (randomNumber >= rangeMin && randomNumber < rangeMax)
            {
                return i;
            }
            rangeMin = rangeMax;
        }

        // Default to the first index (easiest enemy)
        return 0;
    }


    private int EnemiesPerWave()
    {
        return Mathf.RoundToInt(baseEnemies * Mathf.Pow(currentWave, difficultyScalingFactor));
    }
}
