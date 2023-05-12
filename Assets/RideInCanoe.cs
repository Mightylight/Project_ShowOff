using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RideInCanoe : MonoBehaviour
{
    [SerializeField] Canoe canoe;

    private void FixedUpdate()
    {
       if(canoe.synced) transform.position = new Vector3 (canoe.transform.position.x, transform.position.y, canoe.transform.position.z);
    }
}
