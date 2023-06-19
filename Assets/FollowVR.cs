using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowVR : MonoBehaviour
{
    [SerializeField] private Camera _VRcam;
    [SerializeField] float speed = 0.8f;
   
    void Update()
    {
        float angleDif = Quaternion.Angle(transform.rotation, _VRcam.transform.rotation);
        var step = (angleDif / speed) * Time.unscaledDeltaTime;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, new Quaternion(0, _VRcam.transform.rotation.y, 0, _VRcam.transform.rotation.w), step);
    }
}
