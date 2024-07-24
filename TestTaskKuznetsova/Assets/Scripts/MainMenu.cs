using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public TextMeshProUGUI highScoreText;

    void Start()
    {
        if (GameManager.instance != null && highScoreText != null)
        {
            highScoreText.text = "High Score: " + GameManager.instance.GetHighScore();
        }
    }

    public void StartGame()
    {
        if (GameManager.instance != null)
        {
            SceneManager.LoadScene("GameScene");
        }
    }
}