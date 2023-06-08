using System.Collections;
using System.Collections.Generic;
using Canoe;
using UnityEngine;

public class SkyboxMaskScript : MonoBehaviour
{
    [SerializeField] private Transform _canoe;

    [SerializeField] private float _frontOffset = 0f;
    [SerializeField] private float _backOffset = 0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = transform.position;
        position.z = _canoe.position.z;
        transform.position = position;
    }
}
