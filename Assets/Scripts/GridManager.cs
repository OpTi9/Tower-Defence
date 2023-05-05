using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows;
    public int columns;
    public float cellSize;
    public GameObject squarePrefab;
    public float squareSize;
    
    private int[,] grid;
    
    public TowerFactory towerFactory;

    void Start()
    {
        CreateGrid();
    }
    
    void Update()
    {
        HandleMouseClick();
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

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mouseWorldPosition = GetMouseWorldPosition();
            Vector2Int gridPosition = GetGridPosition(mouseWorldPosition);

            if (gridPosition.x >= 0 && gridPosition.x < rows && gridPosition.y >= 0 && gridPosition.y < columns)
            {
                PlaceTower(gridPosition);
            }
        }
    }

    
    private void PlaceTower(Vector2Int gridPosition)
    {
        if (grid[gridPosition.x, gridPosition.y] == 0) // Check if the cell is empty
        {
            Vector3 worldPosition = GetWorldPosition(gridPosition.x, gridPosition.y);
            towerFactory.CreateNormalTower(worldPosition);
            grid[gridPosition.x, gridPosition.y] = 1; // Mark the cell as occupied
        }
    }
    
    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        int row = Mathf.FloorToInt(worldPosition.x / cellSize);
        int column = Mathf.FloorToInt(worldPosition.y / cellSize);
        return new Vector2Int(row, column);
    }

    void OnDrawGizmos()
    {
        if (grid != null)
        {
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < columns; j++)
                {
                    Gizmos.color = (grid[i, j] == 0) ? Color.white : Color.black;
                    Gizmos.DrawCube(GetWorldPosition(i, j), Vector3.one * cellSize * 0.9f);
                }
            }
        }
    }
}
