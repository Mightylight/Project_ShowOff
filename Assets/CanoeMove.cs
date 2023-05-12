using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanoeMove : MonoBehaviour
{
    public Paddle[] paddles;
    [SerializeField] Rigidbody rb;
    Vector3 velocity;

    private void Awake()
    {
        paddles = GetComponentsInChildren<Paddle>();
    }

    private void FixedUpdate()
    {
        foreach (Paddle p in paddles)
        {
            velocity += p.getThrust();
        }

        rb.MovePosition(rb.position + velocity * Time.fixedDeltaTime);
    }
}
