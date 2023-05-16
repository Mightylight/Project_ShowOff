using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool _gameOver = false;
    [SerializeField] GameObject[] _destroyObjects;

    void Start()
    {
        // Find all objects in the scene that are marked with DontDestroyOnLoad
        

    }
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
        foreach(GameObject gameObject in _destroyObjects)
        {
            Destroy(gameObject);
        }
        SceneManager.LoadScene(currentScene.buildIndex);
    }

    public void SetGameOver()
    {
        _gameOver = true;
    }
}
