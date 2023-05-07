using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AStarPathfinding pathfinding;
    public Vector2Int startCell;
    public Vector2Int endCell;
    
    public WaveManager waveBuilder;

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
        Invoke("StartWave", 5f);
    }
    
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }


    private void StartWave()
    {
        // Calculate the path
        List<Vector2Int> path = pathfinding.FindPath(startCell, endCell);
        // Print the path to the console
        Debug.Log("Path: " + string.Join(" -> ", path));
        // Start spawning enemies
        waveBuilder.StartSpawningEnemies(path);
    }
}