using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SectionUpdate : MonoBehaviour
{
    // Skybox Material
    public Material skyboxMaterial;

    // Post Processing Volumes
    public Volume startVolume;
    public Volume endVolume;

    // Start is called before the first frame update
    void Start()
    {
        skyboxMaterial.SetFloat("_BlendAmount", 0f);

        // Set blend amount to 1 for the start volume
        startVolume.weight = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        // If J key is pressed

        // if (Input.GetKeyDown(KeyCode.J))
        // {
        //     // Start coroutine
        //     StartCoroutine(OnMidSkyboxFade());
        // }
    }

    IEnumerator OnMidSkyboxFade()
    {
        // Get _BlendAmount property
        float blendAmount = skyboxMaterial.GetFloat("_BlendAmount");
        // Fade skybox from 0 to 0.5
        while (blendAmount < 0.5f)
        {
            blendAmount += 0.01f;
            skyboxMaterial.SetFloat("_BlendAmount", blendAmount);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator OnFinalSkyboxFade()
    {
        // Get _BlendAmount property
        float blendAmount = skyboxMaterial.GetFloat("_BlendAmount");
        // Fade skybox from 0.5 to 1
        while (blendAmount < 1f)
        {
            blendAmount += 0.01f;
            skyboxMaterial.SetFloat("_BlendAmount", blendAmount);
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator OnStartVolumeFadeOut()
    {
        // Get weight property
        float weight = startVolume.weight;
        // Fade start volume from 1 to 0
        while (weight > 0f)
        {
            weight -= 0.01f;
            startVolume.weight = weight;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator OnEndVolumeFadeIn()
    {
        // Get weight property
        float weight = endVolume.weight;
        // Fade end volume from 0 to 1
        while (weight < 1f)
        {
            weight += 0.01f;
            endVolume.weight = weight;
            yield return new WaitForSeconds(0.01f);
        }
    }

    public void OnMidSectionEnter()
    {
        StartCoroutine(OnMidSkyboxFade());
        StartCoroutine(OnStartVolumeFadeOut());
    }

    public void OnFinalSectionEnter()
    {
        StartCoroutine(OnFinalSkyboxFade());
        StartCoroutine(OnEndVolumeFadeIn());
    }
}
