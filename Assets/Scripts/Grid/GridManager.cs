using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows;
    public int columns;
    public float cellSize;
    public GameObject squarePrefab;
    public float squareSize;
    
    public int[,] grid;

    public static GridManager Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        CreateGrid();
    }

    private void CreateGrid()
    {
        grid = new int[rows, columns];

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                grid[i, j] = 0; // Empty state
                Vector3 worldPosition = GetWorldPosition(i, j);
                GameObject square = Instantiate(squarePrefab, worldPosition, Quaternion.identity);
                square.transform.SetParent(transform);
                square.transform.localScale = new Vector3(squareSize, squareSize, 1);
            }
        }
    }

    public Vector3 GetWorldPosition(int row, int column)
    {
        return new Vector3(row * cellSize + cellSize / 2, column * cellSize + cellSize / 2);
    }
    
    private Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = -Camera.main.transform.position.z;
        return Camera.main.ScreenToWorldPoint(mousePosition);
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        int row = Mathf.FloorToInt(worldPosition.x / cellSize);
        int column = Mathf.FloorToInt(worldPosition.y / cellSize);
        return new Vector2Int(row, column);
    }
    
    public bool IsWithinBounds(Vector2Int position)
    {
        return position.x >= 0 && position.x < rows && position.y >= 0 && position.y < columns;
    }
    
}
