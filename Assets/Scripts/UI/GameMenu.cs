using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;
    
    [SerializeField] private Image soundVolume;
    [SerializeField] private Image musicVolume;

    [SerializeField] public UnityEvent OnPlayButtonClick;

    private int currentSelectedIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayButtonClick()
    {
        SceneManager.LoadScene("PlayTest");
    }
}
