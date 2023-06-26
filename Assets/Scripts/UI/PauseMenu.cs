using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused = false;

    [SerializeField] private Image soundVolume;
    [SerializeField] private Image musicVolume;

    [SerializeField] public UnityEvent OnMainMenuButtonClick;

    private int currentSelectedIndex = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        //Time.timeScale = 0f;
        isPaused = true;
    }

    void Resume()
    {
        pauseMenuUI.SetActive(false);
        //Time.timeScale = 1f;
        isPaused = false;
    }

    // Whenever the joystick is moved left, the sound volume is reduced.
    void OnSoundVolumeLeft()
    {

    }

    // Whenever the joystick is moved right, the sound volume is increased.
    void OnSoundVolumeRight()
    {

    }

    // Whenever the joystick is moved left, the music volume is reduced.
    void OnMusicVolumeLeft()
    {

    }

    // Whenever the joystick is moved right, the music volume is increased.
    void OnMusicVolumeRight()
    {

    }

    public void MainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
