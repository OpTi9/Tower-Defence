using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AStarPathfinding pathfinding;
    public Vector2Int startCell;
    public Vector2Int endCell;
    
    public WaveBuilder waveBuilder;

    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        Building,
        Wave
    }
    
    public GameState currentState;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        currentState = GameState.Building;
    }
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleGameState();
        }
    }

    public void ToggleGameState()
    {
        currentState = (currentState == GameState.Building) ? GameState.Wave : GameState.Building;
        if (currentState == GameState.Wave)
        {
            // Calculate the path
            List<Vector2Int> path = pathfinding.FindPath(startCell, endCell);
            // Print the path to the console
            Debug.Log("Path: " + string.Join(" -> ", path));
            // Spawn enemies
            StartCoroutine(SpawnEnemies(path));
        }
    }
    
    private IEnumerator SpawnEnemies(List<Vector2Int> path)
    {
        List<Enemy> enemies = waveBuilder.BuildWave();
        foreach (Enemy enemy in enemies)
        {
            enemy.Move(path);
            yield return new WaitForSeconds(waveBuilder.spawnDelay);
        }
    }

}