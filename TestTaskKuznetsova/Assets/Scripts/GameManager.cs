using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GameManager : MonoBehaviour

{
    public static GameManager instance;
    private string highScoreFilePath;
    private int highScore;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            highScoreFilePath = Path.Combine(Application.persistentDataPath, "highscore.json");
            LoadHighScore();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetHighScore()
    {
        return highScore;
    }

    public bool UpdateHighScore(int score)
    {
        if (score > highScore)
        {
            highScore = score;
            SaveHighScore();
            return true;
        }
        return false;
    }

    private void SaveHighScore()
    {
        HighScoreData data = new HighScoreData { highScore = highScore };
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(highScoreFilePath, json);
    }

    private void LoadHighScore()
    {
        if (File.Exists(highScoreFilePath))
        {
            string json = File.ReadAllText(highScoreFilePath);
            HighScoreData data = JsonUtility.FromJson<HighScoreData>(json);
            highScore = data.highScore;
        }
    }

    [System.Serializable]
    private class HighScoreData
    {
        public int highScore;
    }

    public void SaveCurrentScore(int score)
    {
        UpdateHighScore(score);
    }
}