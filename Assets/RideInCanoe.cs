using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideInCanoe : MonoBehaviour
{
    [SerializeField] Transform canoe;

    private void FixedUpdate()
    {
        transform.position = new Vector3 (canoe.position.x, transform.position.y, canoe.position.z);
    }
}
