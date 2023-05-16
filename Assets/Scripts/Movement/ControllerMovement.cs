using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.InputSystem.XR;

public class ControllerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    [SerializeField] private float _speed = 5;
    private Vector3 _inputs = Vector3.zero;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Turn();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Turn()
    {
        _inputs = Vector3.zero;
        _inputs.x = Input.GetAxis("Horizontal2");
        _inputs.z = Input.GetAxis("Vertical2");
        
        if(_inputs.z < 0)
        {
            _inputs.z = 0;
        }

        if(_inputs != Vector3.zero)
        {
            transform.forward = _inputs;
        }


    }

    private void Move()
    {
        _rb.MovePosition(_rb.position + _inputs * _speed * Time.fixedDeltaTime);
    }
}
