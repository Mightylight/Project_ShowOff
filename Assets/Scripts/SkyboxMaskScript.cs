using System.Collections;
using System.Collections.Generic;
using Canoe;
using UnityEngine;

public class SkyboxMaskScript : MonoBehaviour
{
    [SerializeField] private Transform _canoe;
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
