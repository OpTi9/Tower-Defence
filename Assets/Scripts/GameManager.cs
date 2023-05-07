using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public AStarPathfinding pathfinding;
    public Vector2Int startCell;
    public Vector2Int endCell;
    
    public GameObject startCellPrefab;
    public GameObject endCellPrefab;
    
    public WaveManager waveManager;

    public static GameManager Instance { get; private set; }

    public enum GameState
    {
        Building,
        Wave
    }
    
    public GameState currentState;
    
    public List<Vector2Int> GetPath()
    {
        return pathfinding.FindPath(startCell, endCell);
    }

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
        PlaceStartAndEndCellPrefabs();
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
        waveManager.StartSpawningEnemies();
    }
    
    private void PlaceStartAndEndCellPrefabs()
    {
        GridManager gridManager = GridManager.Instance;
        Vector3 startCellPosition = gridManager.GetWorldPosition(startCell.x, startCell.y);
        Vector3 endCellPosition = gridManager.GetWorldPosition(endCell.x, endCell.y);

        Instantiate(startCellPrefab, startCellPosition, Quaternion.identity);
        Instantiate(endCellPrefab, endCellPosition, Quaternion.identity);
    }
}