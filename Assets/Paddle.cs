using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    bool paddling = false;
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
