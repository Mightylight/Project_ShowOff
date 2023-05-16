using UnityEngine;
using UnityEngine.SpatialTracking;

namespace Canoe
{
    public class LockPosition : MonoBehaviour
    {
        private TrackedPoseDriver _tpd;
        private float _timeLeft = 1.0f;

        private void Awake()
        {
            _tpd = GetComponent<TrackedPoseDriver>();
        }
        private void LateUpdate()
        {
            _timeLeft -= Time.deltaTime;
            if (_timeLeft <= 0)
            {
                _tpd.trackingType = TrackedPoseDriver.TrackingType.RotationOnly;
            }
        }
    }
}
