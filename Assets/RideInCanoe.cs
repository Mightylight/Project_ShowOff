using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideInCanoe : MonoBehaviour
{
    [SerializeField] Canoe canoeSync;
    [SerializeField] Rigidbody canoe;

    private void FixedUpdate()
    {
        if (canoeSync.synced)
        {
            transform.position = new Vector3(canoe.transform.position.x, transform.position.y, canoe.transform.position.z);
            transform.rotation = canoe.transform.rotation;
        }

    }
}
