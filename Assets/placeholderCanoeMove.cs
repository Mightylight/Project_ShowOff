using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class placeholderCanoeMove : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 5;
    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
    private void FixedUpdate()
    {
        //Debug.Log(input.axis.x + ", " + input.axis.y);
        rb.MovePosition(rb.position + new Vector3(transform.forward.x, 0, transform.forward.z)*speed*Time.fixedDeltaTime);
    }
}
