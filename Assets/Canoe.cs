using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canoe : MonoBehaviour
{
    [SerializeField] Transform vrCam;

    private void Update()
    {
        transform.position = new Vector3(vrCam.position.x, transform.position.y, vrCam.position.z);
    }
}
