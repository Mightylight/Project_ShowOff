using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Current : MonoBehaviour
{
    Vector3 current = new Vector3(0, 0, 2);
    Rigidbody rb;


    private void Start()
    {
        rb= GetComponent<Rigidbody>();
    }

    private void Update()
    {
        rb.MovePosition(transform.position + current);
    }
}
