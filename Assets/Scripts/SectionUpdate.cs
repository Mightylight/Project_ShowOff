using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SectionUpdate : MonoBehaviour
{
    // Skybox material
    public Material skyboxMaterial;

    // Start is called before the first frame update
    void Start()
    {
        skyboxMaterial.SetFloat("_BlendAmount", 0f);
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

    public void OnMidSectionEnter()
    {
        StartCoroutine(OnMidSkyboxFade());
    }

    public void OnFinalSectionEnter()
    {
        StartCoroutine(OnFinalSkyboxFade());
    }
}
