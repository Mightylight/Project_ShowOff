using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SpatialTracking;

public class LockPosition : MonoBehaviour
{
    TrackedPoseDriver tpd;
    float timeLeft = 1.0f;

    private void Awake()
    {
        tpd = GetComponent<TrackedPoseDriver>();
    }
    private void LateUpdate()
    {
        timeLeft -= Time.deltaTime;
        if (timeLeft <= 0)
        {
            tpd.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
        }
    }
}
