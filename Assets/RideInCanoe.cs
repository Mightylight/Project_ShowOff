using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideInCanoe : MonoBehaviour
{
    [SerializeField] CanoeSync canoeSync;
    [SerializeField] Rigidbody canoe;
    Vector3 offset;

    private void FixedUpdate()
    {
        //if (canoeSync.synced)
        {
            offset = canoeSync.transform.position;
            //Debug.Log(offset);
            transform.position = new Vector3(canoe.transform.position.x, transform.position.y, canoe.transform.position.z);
            transform.rotation = canoe.transform.rotation;
        }

    }
}
