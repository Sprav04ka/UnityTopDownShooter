using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 1;
    public GameObject deathUI;
    public TextMeshProUGUI scoreTextOnDeathUI;
    public TextMeshProUGUI newScoreText;
    public bool isInvincible = false;



    void Start()
    {
        if (deathUI != null)
        {
            deathUI.SetActive(false);
        }

        if (newScoreText != null)
        {
            newScoreText.gameObject.SetActive(false);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Die()
    {
        int currentScore = 0;
        if (ScoreManager.instance != null)
        {
            currentScore = ScoreManager.instance.GetScore();
            if (GameManager.instance != null)
            {
                bool isNewHighScore = GameManager.instance.UpdateHighScore(currentScore);
                ScoreManager.instance.SaveCurrentScore();

                if (isNewHighScore && newScoreText != null)
                {
                    newScoreText.gameObject.SetActive(true);
                }
            }
        }

        if (deathUI != null)
        {
            deathUI.SetActive(true);
            if (scoreTextOnDeathUI != null)
            {
                scoreTextOnDeathUI.text = "Score: " + currentScore;
            }
        }

        Time.timeScale = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy") && !isInvincible)
        {
            TakeDamage(1);
        }
        else if (other.CompareTag("Enemy") && isInvincible)
        {
            Destroy(other.gameObject);
        }
    }

    public void RestartGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        

    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("MainMenu");
    }
    
}