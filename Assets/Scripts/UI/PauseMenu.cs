using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    //private JoystickControls joystickControls;
    [SerializeField] private GameObject volumeMusicSelected;
    [SerializeField] private GameObject volumeSoundSelected;
    [SerializeField] private GameObject exitGameSelected;

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused = false;

    [SerializeField] private Image soundVolume;
    [SerializeField] private Image musicVolume;

    [SerializeField] public UnityEvent OnMainMenuButtonClick;

    private int currentSelectedIndex = 0;

    private bool joystickHeldDown = false;
    private bool joystickHeldUp = false;
    private bool joystickHeldLeft = false;
    private bool joystickHeldRight = false;
    private float joystickThreshold = 0.75f;

    // Start is called before the first frame update
    void Start()
    {
        //joystickControls = new JoystickControls();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("Horizontal: " + Input.GetAxis("Horizontal"));
        Debug.Log("Vertical: " + Input.GetAxis("Vertical"));

        // If joystick moved down, move to next button.
        float verticalInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        if (verticalInput < -joystickThreshold)
        {
            if (!joystickHeldDown)
            {
                joystickHeldDown = true;
                joystickHeldUp = false;
                joystickHeldLeft = false;
                joystickHeldRight = false;

                if (currentSelectedIndex < 2)
                {
                    currentSelectedIndex++;
                }
                else
                {
                    currentSelectedIndex = 0;
                }
            }
        }
        else if (verticalInput > joystickThreshold)
    {
        if (!joystickHeldUp)
        {
            joystickHeldUp = true;
            joystickHeldDown = false;
            joystickHeldLeft = false;
            joystickHeldRight = false;

            if (currentSelectedIndex > 0)
            {
                currentSelectedIndex--;
            }
            else
            {
                    currentSelectedIndex = 2;
                }
            }
        }
        else if (horizontalInput < -joystickThreshold)
        {
            if (!joystickHeldLeft)
            {
                joystickHeldLeft = true;
                joystickHeldDown = false;
                joystickHeldUp = false;
                joystickHeldRight = false;

                if (currentSelectedIndex == 0)
                {
                    OnMusicVolumeLeft();
                }
                else if (currentSelectedIndex == 1)
                {
                    OnSoundVolumeLeft();
                }
            }
        }
        else if (horizontalInput > joystickThreshold)
        {
            if (!joystickHeldRight)
            {
                joystickHeldRight = true;
                joystickHeldDown = false;
                joystickHeldUp = false;
                joystickHeldLeft = false;

                if (currentSelectedIndex == 0)
                {
                    OnMusicVolumeRight();
                }
                else if (currentSelectedIndex == 1)
                {
                    OnSoundVolumeRight();
                }
            }
        }
        else if (verticalInput >= -joystickThreshold && verticalInput <= joystickThreshold
            && horizontalInput >= -joystickThreshold && horizontalInput <= joystickThreshold)
        {
            joystickHeldDown = false;
            joystickHeldUp = false;
            joystickHeldLeft = false;
            joystickHeldRight = false;
        }

        // Switch currentSelectedIndex
        switch (currentSelectedIndex)
        {
            case 0:
                volumeMusicSelected.SetActive(true);
                volumeSoundSelected.SetActive(false);
                exitGameSelected.SetActive(false);
                break;
            case 1:
                volumeMusicSelected.SetActive(false);
                volumeSoundSelected.SetActive(true);
                exitGameSelected.SetActive(false);
                break;
            case 2:
                volumeMusicSelected.SetActive(false);
                volumeSoundSelected.SetActive(false);
                exitGameSelected.SetActive(true);
                break;
        }
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

    // Whenever the joystick is moved left, the music volume is reduced.
    void OnMusicVolumeLeft()
    {
        Debug.Log("OnMusicVolumeLeft");
        // Decrease fill amount of music volume image.
        if (musicVolume.fillAmount > 0)
        {
            musicVolume.fillAmount -= 0.1f;
            //audioManager.SetMusicVolume(musicVolume.fillAmount);
        }
    }

    // Whenever the joystick is moved right, the music volume is increased.
    void OnMusicVolumeRight()
    {
        Debug.Log("OnMusicVolumeRight");
        // Increase fill amount of music volume image.
        if (musicVolume.fillAmount < 1)
        {
            musicVolume.fillAmount += 0.1f;
            //audioManager.SetMusicVolume(musicVolume.fillAmount);
        }
    }

    // Whenever the joystick is moved left, the sound volume is reduced.
    void OnSoundVolumeLeft()
    {
        Debug.Log("OnSoundVolumeLeft");
        if (soundVolume.fillAmount > 0)
        {
            soundVolume.fillAmount -= 0.1f;
            //audioManager.SetSoundVolume(soundVolume.fillAmount);
        }
    }

    // Whenever the joystick is moved right, the sound volume is increased.
    void OnSoundVolumeRight()
    {
        Debug.Log("OnSoundVolumeRight");
        if (soundVolume.fillAmount < 1)
        {
            soundVolume.fillAmount += 0.1f;
            //audioManager.SetSoundVolume(soundVolume.fillAmount);
        }
    }

    public void MainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
