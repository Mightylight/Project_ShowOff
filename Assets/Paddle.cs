using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] Transform paddleTip;
    bool paddling = false;
    Vector3 lastPosition = Vector3.zero;
    Vector3 currentPosition = Vector3.zero;
    Vector3 thrust;
    [SerializeField] float strength = 1000;


    private void FixedUpdate()
    {
        if(paddling) 
        {
            lastPosition = currentPosition;
            currentPosition = paddleTip.position;
            //Debug.Log(lastPosition - currentPosition);
            thrust = -strength * (lastPosition - currentPosition);
            thrust.y = 0;
        }
        else
        {
            lastPosition = Vector3.zero;
            currentPosition = Vector3.zero;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("paddleZone"))
        {
            Debug.Log("paddlin");
            paddling= true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("paddleZone"))
        {
            Debug.Log("nope to paddlin");
            paddling = false;
        }
    }

    public Vector3 getThrust()
    {
        //Debug.Log(thrust.ToString());
        return thrust;
    }
    public bool IsPaddling() { return paddling; }
}
