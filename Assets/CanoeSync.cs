using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanoeSync : MonoBehaviour 
{
    [SerializeField] Transform vrCam;
    [SerializeField] float yLocation = -0.5f;
    public bool synced = false;
    [SerializeField] float timeLeft = 1.0f;
    private void Update()
    {        
        if (timeLeft < 0) synced = true;
        else timeLeft -= Time.fixedDeltaTime;
        if(!synced)
        {
            transform.localPosition = new Vector3(vrCam.position.x, vrCam.position.y + yLocation, vrCam.position.z);
            transform.localRotation = Quaternion.Euler(0, vrCam.localEulerAngles.y, 0); //vrCam.localEulerAngles
            //transform.localRotation = vrCam.localRotation;

            //Quaternion.EulerAngles
        }
        
    }
    //{
    //    
    //    public bool synced = false;
    //    [SerializeField] float timeLeft = 1.0f;
    //    [SerializeField] GameObject canoe;
    //    private void Start()
    //    {

    //    }
    //    private void FixedUpdate()
    //    {
    //        timeLeft -= Time.fixedDeltaTime;
    //        if (timeLeft < 0) synced = true;
    //        if(!synced) canoe.transform.position = new Vector3(vrCam.position.x, vrCam.position.y - 0.7f, vrCam.position.z);

    //    }

    //    private void OnTriggerEnter(Collider other)
    //    {
    //       // if (other.CompareTag("player"))synced= true;
    //    }
}
