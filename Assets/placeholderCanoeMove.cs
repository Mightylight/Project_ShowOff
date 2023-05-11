using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        rb.MovePosition(rb.position + transform.forward*speed*Time.fixedDeltaTime);
    }
}
