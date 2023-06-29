using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPosition : MonoBehaviour
{
    [SerializeField] Transform _syncTo;
    [SerializeField] private bool _isRotating = true;
    [SerializeField] private bool _VRRot = false;
    [SerializeField] float speed = 0.8f;


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
        else if (_VRRot)
        {
            float angleDif = Quaternion.Angle(transform.rotation, _syncTo.rotation);
            var step = (angleDif / speed) * Time.unscaledDeltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, _syncTo.rotation.y, 0, _syncTo.rotation.w), step);
        }
    }
}
