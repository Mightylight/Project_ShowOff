using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool _gameOver = false;
    void Update()
    {
        if (Input.anyKeyDown && _gameOver)
        {
            RestartScene();
        }
    }

    void RestartScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void SetGameOver()
    {
        _gameOver = true;
    }
}
