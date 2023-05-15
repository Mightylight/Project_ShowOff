using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionModification : MonoBehaviour
{
    private void Awake()
    {
        Physics.IgnoreLayerCollision(3, 3);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
    }
}
