using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int playerHealth;
    [SerializeField] TextMeshProUGUI healthUI;
    
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
        Wave,
        GameOver
    }
    
    private void Update()
    {
        if (currentState == GameState.GameOver)
        {
            GameOver();
        }
    }

    
    public GameState currentState;
    
    public List<Vector2Int> GetPath()
    {
        return pathfinding.FindPath(startCell, endCell);
    }
    
    private void GameOver()
    {
        // Show the game over UI
        // Add logic for restarting the game or going back to the main menu
        Debug.Log("game over");
    }

    
    private void OnGUI()
    {
        healthUI.text = playerHealth.ToString();
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

    public void ChangePlayerHealth(int penalty)
    {
        Debug.Log("Changing player health. Penalty: " + penalty);
        Debug.Log("Current health: " + playerHealth);
        playerHealth = playerHealth - penalty;
        Debug.Log("new health: " + playerHealth);
        if (playerHealth <= 0)
        {
            GameManager.Instance.currentState = GameState.GameOver;
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

        // Randomize the start cell's column and set the row to 0 (first row)
        startCell = new Vector2Int(0, Random.Range(0, gridManager.columns));
        Vector3 startCellPosition = gridManager.GetWorldPosition(startCell.x, startCell.y);

        // Randomize the end cell's column and set the row to the last row
        endCell = new Vector2Int(gridManager.rows - 1, Random.Range(0, gridManager.columns));
        Vector3 endCellPosition = gridManager.GetWorldPosition(endCell.x, endCell.y);

        Instantiate(startCellPrefab, startCellPosition, Quaternion.identity);
        Instantiate(endCellPrefab, endCellPosition, Quaternion.identity);
    }


}