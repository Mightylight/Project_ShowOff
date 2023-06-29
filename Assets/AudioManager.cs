using UnityEngine.Audio;
using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{

    public Sound[] sounds;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.outputAudioMixerGroup = s.mGroup;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;

            s.source.loop = s.loop;
        }
    }

    void Start ()
    {
        Play("Ambience 1");
    }


    void OnEnable () 
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded (Scene scene, LoadSceneMode mode) {
        
        Scene currentScene = SceneManager.GetActiveScene ();
        //string sceneName = currentScene.name;
 
 
        if (currentScene.buildIndex == 1) 
        {
            Play("Ingame Theme");
        }
        
        else if (currentScene.buildIndex == 0) 
        {
            Stop("Ingame Theme");
        }
        

    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found!");
            return;
        }
    
        s.source.Play();

        /*
        Play sound from other scripts:
        FindObjectOfType<AudioManager>().Play("Sound.name");
        */
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound:" + name + "not found! (check for typos)" );
            return;
        }
      
        s.source.Stop();
    }
}
