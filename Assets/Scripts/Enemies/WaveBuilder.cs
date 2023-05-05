using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveBuilder : MonoBehaviour
{
    public Enemy enemyPrefab;
    public int enemyCount;
    public float spawnDelay;

    public List<Enemy> BuildWave()
    {
        List<Enemy> wave = new List<Enemy>();
        for (int i = 0; i < enemyCount; i++)
        {
            Enemy newEnemy = Instantiate(enemyPrefab);
            wave.Add(newEnemy);
        }
        return wave;
    }
}
