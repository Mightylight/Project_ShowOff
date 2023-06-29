using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SyncPosition : MonoBehaviour
{
    [SerializeField] Transform _syncTo;
    [SerializeField] private bool _isRotating = true;
    [SerializeField] private bool _isMoving = true;
    [SerializeField] private bool _VRRot = false;
    [SerializeField] float speed = 0.8f;
    [SerializeField] private bool _onlyYrot = false;


    private void Update()
    {
        Sync();
    }
    void Sync()
    {
        if (_isMoving) transform.position = _syncTo.position;
        if (_isRotating)
        {
            if (_onlyYrot)
            {
                transform.rotation = Quaternion.Euler(0, _syncTo.rotation.y, 0);
            }
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
