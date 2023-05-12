using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] Transform paddleTip;
    bool paddling = false;


    private void Update()
    {
        //if (paddleTip.position.y < 0) paddling= true;
        //else paddling= false;
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

    public bool IsPaddling() { return paddling; }
}
