using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPosition : MonoBehaviour
{
    [SerializeField] Transform _syncTo;

    private void Update()
    {
        Sync();
    }
    void Sync()
    {
        transform.position = _syncTo.position;
        transform.rotation = _syncTo.rotation;
    }
}
