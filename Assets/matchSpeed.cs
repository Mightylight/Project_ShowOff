using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matchSpeed : MonoBehaviour
{
    [SerializeField] placeholderCanoeMove move;
    Rigidbody rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        rb.MovePosition(move.move);
    }
}
