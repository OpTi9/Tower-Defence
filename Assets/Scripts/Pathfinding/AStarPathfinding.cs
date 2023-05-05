using System.Collections.Generic;
using UnityEngine;

public class AStarPathfinding : MonoBehaviour
{
    public GridManager gridManager;
    private int[,] grid => gridManager.grid;
    
    private Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int(1, 0),
        new Vector2Int(0, 1),
        new Vector2Int(-1, 0),
        new Vector2Int(0, -1)
    };

    public List<Vector2Int> FindPath(Vector2Int start, Vector2Int end)
    {
        Dictionary<Vector2Int, Vector2Int> cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        Dictionary<Vector2Int, float> costSoFar = new Dictionary<Vector2Int, float>();

        Dictionary<Vector2Int, float> priorityValues = new Dictionary<Vector2Int, float>();
        Vector2IntComparer comparer = new Vector2IntComparer(priorityValues);
        SortedSet<Vector2Int> frontier = new SortedSet<Vector2Int>(comparer);
        
        priorityValues[start] = 0;
        frontier.Add(start);

        cameFrom[start] = start;
        costSoFar[start] = 0;

        while (frontier.Count > 0)
        {
            Vector2Int current = frontier.Min;
            frontier.Remove(current);

            if (current == end)
            {
                break;
            }

            foreach (Vector2Int direction in directions)
            {
                
                Vector2Int next = current + direction;
                if (gridManager.IsWithinBounds(next) && grid[next.x, next.y] == 0)
                {
                    float newCost = costSoFar[current] + 1;
                    if (!costSoFar.ContainsKey(next) || newCost < costSoFar[next])
                    {
                        costSoFar[next] = newCost;
                        float priority = newCost + Heuristic(end, next);
                        priorityValues[next] = priority;
                        frontier.Add(next);
                        cameFrom[next] = current;
                    }

                }
            }
        }

        return ReconstructPath(cameFrom, start, end);
    }

    private float Heuristic(Vector2Int a, Vector2Int b)
    {
        return Mathf.Abs(a.x - b.x) + Mathf.Abs(a.y - b.y);
    }

    private List<Vector2Int> ReconstructPath(Dictionary<Vector2Int, Vector2Int> cameFrom, Vector2Int start, Vector2Int end)
    {
        List<Vector2Int> path = new List<Vector2Int>();
        Vector2Int current = end;

        while (current != start)
        {
            path.Add(current);
            current = cameFrom[current];
        }

        path.Add(start);
        path.Reverse();

        return path;
    }
}
