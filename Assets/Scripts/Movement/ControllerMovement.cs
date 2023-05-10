using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class ControllerMovement : MonoBehaviour
{
    Rigidbody rb;
    [SerializeField] float speed = 5;
    Vector3 inputs = Vector3.zero;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        turn();
    }

    private void FixedUpdate()
    {
        move();
    }

    void turn()
    {
        inputs = Vector3.zero;
        inputs.x = Input.GetAxis("Horizontal2");
        inputs.z = Input.GetAxis("Vertical2");

        if(inputs != Vector3.zero)
        {
            transform.forward = inputs;
        }


    }

    void move()
    {
        rb.MovePosition(rb.position + inputs * speed * Time.fixedDeltaTime);
    }
}
