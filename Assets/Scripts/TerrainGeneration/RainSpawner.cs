using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RainSpawner : MonoBehaviour
{
    GameObject[] rain;

    // Start is called before the first frame update
    void Start()
    {
        // Disable all objects with tag rain
        rain = GameObject.FindGameObjectsWithTag("Rain");

        // 50% chance of rain
        if (Random.Range(0, 2) == 0)
        {
            DisableRain();
        }
        else
        {
            EnableRain();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DisableRain()
    {
        foreach (GameObject r in rain)
        {
            r.SetActive(false);
        }
    }

    void EnableRain()
    {
        foreach (GameObject r in rain)
        {
            r.SetActive(true);
        }
    }
}
