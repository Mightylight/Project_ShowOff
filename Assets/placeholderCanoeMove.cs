using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class placeholderCanoeMove : MonoBehaviour
{
    [SerializeField]Rigidbody rb;
    [SerializeField] float speed = 5;
    public Vector3 move;
    private void Awake()
    {
       
    }
    private void FixedUpdate()
    {
        //Debug.Log(input.axis.x + ", " + input.axis.y);
        move = rb.position + new Vector3(transform.forward.x, 0, transform.forward.z) * speed * Time.fixedDeltaTime;
        rb.MovePosition(move);
    }
}
