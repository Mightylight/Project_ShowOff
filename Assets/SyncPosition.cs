using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPosition : MonoBehaviour
{
    [SerializeField] Transform _syncTo;
    [SerializeField] private bool _isRotating = true;
    

    private void Update()
    {
        Sync();
    }
    void Sync()
    {
        transform.position = _syncTo.position;
        if (_isRotating)
        {
            transform.rotation = _syncTo.rotation;
        }
    }
}
