using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class PauseMenu : MonoBehaviour
{
    //private JoystickControls joystickControls;
    [SerializeField] private GameObject volumeMusicSelected;
    [SerializeField] private GameObject volumeSoundSelected;
    [SerializeField] private GameObject exitGameSelected;

    [SerializeField] private AudioManager audioManager;
    [SerializeField] public AudioMixerGroup musicMixer;
    [SerializeField] public AudioMixerGroup soundMixer;

    [SerializeField] private GameObject controlsUI;

    [SerializeField] private GameObject pauseMenuUI;
    [SerializeField] private bool isPaused = false;

    [SerializeField] private Image soundVolume;
    [SerializeField] private Image musicVolume;

    [Header("Loading Screen")]
    [SerializeField] private string loadingSceneName = "MainManu";
    [SerializeField] private GameObject loadingScreen;

    private int currentSelectedIndex = 0;

    private bool joystickHeldDown = false;
    private bool joystickHeldUp = false;
    private bool joystickHeldLeft = false;
    private bool joystickHeldRight = false;
    private float joystickThreshold = 0.75f;

    private bool isLoading = false;

    private JoystickControls _joystickControls;

    private void Awake()
    {
        _joystickControls = new JoystickControls();

        _joystickControls.Alligator.Bite.performed += pCtx => PressButton();
        _joystickControls.Alligator.Pause.performed += pCtx => PressPause();
        //_joystickControls.Alligator.Pause.canceled += pCtx => buttonHeldDown = false;

        _joystickControls.Alligator.Enable();
    }

    IEnumerator LoadAsyncScene()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(loadingSceneName);

        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }

    void PressButton()
    {
        if (isPaused)
        {
            return;
        }

        if (currentSelectedIndex == 2)
        {
            loadingScreen.SetActive(true);
            controlsUI.SetActive(false);

            isLoading = true;

            StartCoroutine(LoadAsyncScene());
        }
    }

    void PressPause()
    {
        Debug.Log("Pause button pressed.");

        isPaused = !isPaused;

        if (!isPaused)
        {
            HidePauseMenu();
        }
        else
        {
            ShowPauseMenu();
        }
    }

    void ShowPauseMenu()
    {
        Debug.Log("Showing pause menu.");
        pauseMenuUI.SetActive(true);
        isPaused = true;

        // set time scale to 0
        Time.timeScale = 0f;
    }

    void HidePauseMenu()
    {
        Debug.Log("Hiding pause menu.");
        pauseMenuUI.SetActive(false);
        isPaused = false;

        // set time scale to 1
        Time.timeScale = 1f;
    }

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
            //musicMixer.Setfloat("MusicVolume", musicVolume.fillAmount);
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
            //musicMixer.Setfloat("MusicVolume", musicVolume.fillAmount);
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
            //soundMixer.Setfloat("EffectsMixer", soundVolume.fillAmount);
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
            //soundMixer.Setfloat("EffectsMixer", soundVolume.fillAmount);
        }
    }

    public void MainMenuButtonClick()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
