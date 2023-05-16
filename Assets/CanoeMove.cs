using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanoeMove : MonoBehaviour
{
    public Paddle[] paddles;
    [SerializeField] Rigidbody rb;
  

    private void Awake()
    {
       // paddles = GetComponentsInChildren<Paddle>();
    }

    private void FixedUpdate()
    {
        Vector3 velocity = Vector3.zero;
        foreach (Paddle p in paddles)
        {
            velocity += p.getThrust();
        }

        rb.AddForce(velocity);
    }


    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
