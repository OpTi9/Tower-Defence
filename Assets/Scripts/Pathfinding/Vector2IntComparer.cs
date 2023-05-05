using System.Collections.Generic;
using UnityEngine;

public class Vector2IntComparer : IComparer<Vector2Int>
{
    private Dictionary<Vector2Int, float> costValues;

    public Vector2IntComparer(Dictionary<Vector2Int, float> costValues)
    {
        this.costValues = costValues;
    }

    public int Compare(Vector2Int a, Vector2Int b)
    {
        if (costValues[a] < costValues[b])
        {
            return -1;
        }
        else if (costValues[a] > costValues[b])
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }
}