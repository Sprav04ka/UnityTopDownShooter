using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour

{
    

    void Start()
    {
        
    }
    public void NextScene()
    {
        Time.timeScale = 1;
        
        SceneManager.LoadScene("GameScene");
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        
        SceneManager.LoadScene("MainMenu");
   
    }
}