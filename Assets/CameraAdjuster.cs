using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraAdjuster : MonoBehaviour
{
    
    private Transform _camera;
    // Start is called before the first frame update
    void Awake()
    {
        _camera = transform;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = _camera.position;
        transform.rotation = _camera.rotation;
        transform.localScale = _camera.localScale;
    }
}
