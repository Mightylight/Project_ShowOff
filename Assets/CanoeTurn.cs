using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CanoeTurn : MonoBehaviour
{
    public SteamVR_Action_Vector2 leftInput;
    public SteamVR_Action_Vector2 rightInput;
    public float turnRate = 100f;
    Rigidbody rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        Quaternion deltaRot = Quaternion.Euler(0, (leftInput.axis.y - rightInput.axis.y) * turnRate * Time.fixedDeltaTime, 0);
        rb.MoveRotation(rb.rotation * deltaRot);
    }
}
