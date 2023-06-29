using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    [SerializeField] private AudioManager audioManager;

    [SerializeField] private GameObject mainMenuUI;
    [SerializeField] private GameObject settingsMenuUI;
    [SerializeField] private GameObject controlsUI;
    [SerializeField] private bool isMenuVisible = false;
    [SerializeField] private bool isSettingsVisible = false;

    // Main Menu menu
    [Header("Main Menu")]
    [SerializeField] private GameObject playButtonSelected;
    [SerializeField] private GameObject settingsButtonSelected;
    [SerializeField] private GameObject exitGameSelected;

    // Settings Menu menu
    [Header("Settings Menu")]
    [SerializeField] private GameObject volumeMusicSelected;
    [SerializeField] private GameObject volumeSoundSelected;
    [SerializeField] private GameObject backSelected;
    [SerializeField] private Image soundVolume;
    [SerializeField] private Image musicVolume;

    // Settings Menu menu
    [Header("Loading Screen")]
    [SerializeField] private string loadingSceneName = "Playtest";
    [SerializeField] private GameObject loadingScreen;

    private int currentMenuSelectedIndex = 0;
    private int currentSettingsSelectedIndex = 0;

    private bool buttonHeldDown = false;
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
        _joystickControls.Alligator.Bite.canceled += pCtx => buttonHeldDown = false;

        _joystickControls.Alligator.Enable();
    }

    // Start is called before the first frame update
    void Start()
    {
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
        if (buttonHeldDown || isLoading)
        {
            return;
        }

        buttonHeldDown = true;

        switch (currentMenuSelectedIndex)
        {
            case 0:
                mainMenuUI.SetActive(false);
                settingsMenuUI.SetActive(false);
                loadingScreen.SetActive(true);
                controlsUI.SetActive(false);

                isLoading = true;

                //SceneManager.LoadScene(loadingSceneName);
                StartCoroutine(LoadAsyncScene());

                break;
            case 1:
                currentSettingsSelectedIndex = 0;
                currentMenuSelectedIndex = 0;

                mainMenuUI.SetActive(false);
                settingsMenuUI.SetActive(true);
                break;
            case 2:
                Application.Quit();
                break;
        }

        if (!settingsMenuUI.activeSelf)
        {
            return;
        }

        switch (currentSettingsSelectedIndex)
        {
            case 2:
                currentSettingsSelectedIndex = 0;
                currentMenuSelectedIndex = 0;

                mainMenuUI.SetActive(true);
                settingsMenuUI.SetActive(false);
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
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

                if (currentMenuSelectedIndex < 2)
                {
                    currentMenuSelectedIndex++;
                }
                else
                {
                    currentMenuSelectedIndex = 0;
                }

                if (currentSettingsSelectedIndex < 2)
                {
                    currentSettingsSelectedIndex++;
                }
                else
                {
                    currentSettingsSelectedIndex = 0;
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

                if (currentMenuSelectedIndex > 0)
                {
                    currentMenuSelectedIndex--;
                }
                else
                {
                    currentMenuSelectedIndex = 2;
                }

                if (currentSettingsSelectedIndex > 0)
                {
                    currentSettingsSelectedIndex--;
                }
                else
                {
                    currentSettingsSelectedIndex = 2;
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

        // Switch currentMenuSelectedIndex
        switch (currentMenuSelectedIndex)
        {
            case 0:
                playButtonSelected.SetActive(true);
                settingsButtonSelected.SetActive(false);
                exitGameSelected.SetActive(false);
                break;
            case 1:
                playButtonSelected.SetActive(false);
                settingsButtonSelected.SetActive(true);
                exitGameSelected.SetActive(false);
                break;
            case 2:
                playButtonSelected.SetActive(false);
                settingsButtonSelected.SetActive(false);
                exitGameSelected.SetActive(true);
                break;
        }

        if (horizontalInput < -joystickThreshold)
        {
            if (!joystickHeldLeft)
            {
                joystickHeldLeft = true;
                joystickHeldDown = false;
                joystickHeldUp = false;
                joystickHeldRight = false;

                if (currentSettingsSelectedIndex == 0)
                {
                    OnMusicVolumeLeft();
                }
                else if (currentSettingsSelectedIndex == 1)
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

                if (currentSettingsSelectedIndex == 0)
                {
                    OnMusicVolumeRight();
                }
                else if (currentSettingsSelectedIndex == 1)
                {
                    OnSoundVolumeRight();
                }
            }
        }

        // Switch currentSettingsSelectedIndex
        switch (currentSettingsSelectedIndex)
        {
            case 0:
                volumeMusicSelected.SetActive(true);
                volumeSoundSelected.SetActive(false);
                backSelected.SetActive(false);
                break;
            case 1:
                volumeMusicSelected.SetActive(false);
                volumeSoundSelected.SetActive(true);
                backSelected.SetActive(false);
                break;
            case 2:
                volumeMusicSelected.SetActive(false);
                volumeSoundSelected.SetActive(false);
                backSelected.SetActive(true);
                break;
        }
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
}
