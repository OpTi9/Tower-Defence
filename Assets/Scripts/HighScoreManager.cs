using System.Collections.Generic;
using UnityEngine;

public class HighScoreManager : MonoBehaviour
{
    public static HighScoreManager Instance { get; private set; }

    private const int MaxHighScores = 10;
    private const string HighScoresKey = "HighScores";

    public List<HighScoreEntry> highScores;

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

        LoadHighScores();
    }

    public bool IsHighScore(int score)
    {
        return highScores.Count < MaxHighScores || score > highScores[highScores.Count - 1].score;
    }

    public void AddHighScore(string playerName, int score)
    {
        HighScoreEntry newEntry = new HighScoreEntry { playerName = playerName, score = score };
        highScores.Add(newEntry);
        highScores.Sort((x, y) => y.score.CompareTo(x.score));

        if (highScores.Count > MaxHighScores)
        {
            highScores.RemoveAt(highScores.Count - 1);
        }

        SaveHighScores();
    }

    private void LoadHighScores()
    {
        string json = PlayerPrefs.GetString(HighScoresKey, "");
        if (!string.IsNullOrEmpty(json))
        {
            HighScoresData data = JsonUtility.FromJson<HighScoresData>(json);
            highScores = data.highScores;
        }
        else
        {
            highScores = new List<HighScoreEntry>();
        }
    }

    private void SaveHighScores()
    {
        HighScoresData data = new HighScoresData { highScores = highScores };
        string json = JsonUtility.ToJson(data);
        PlayerPrefs.SetString(HighScoresKey, json);
        PlayerPrefs.Save();
    }

    [System.Serializable]
    public class HighScoreEntry
    {
        public string playerName;
        public int score;
    }

    [System.Serializable]
    private class HighScoresData
    {
        public List<HighScoreEntry> highScores;
    }
}
