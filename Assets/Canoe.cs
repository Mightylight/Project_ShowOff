using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Canoe : MonoBehaviour
{
    [SerializeField] Transform vrCam;
    public bool synced = false;
    [SerializeField] float timeLeft = 1.0f;
    private void Start()
    {
       
    }
    private void FixedUpdate()
    {
        timeLeft -= Time.fixedDeltaTime;
        if (timeLeft < 0) synced = true;
        if(!synced) transform.position = new Vector3(vrCam.position.x, vrCam.position.y - 0.7f, vrCam.position.z);

    }

    private void OnTriggerEnter(Collider other)
    {
       // if (other.CompareTag("player"))synced= true;
    }
}
