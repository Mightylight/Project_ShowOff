using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    [SerializeField] float minPitch;
    [SerializeField] float maxPitch;
    [SerializeField] float pitchSpeed;
    float currentPitch;

    [SerializeField] float yawSpeed;


    void ChangePitch(float angle)
    {
        
        transform.rotation = Quaternion.Euler(angle, 0, 0);
    }

    void ChangeYaw(float angle)
    {
        transform.rotation = Quaternion.Euler(currentPitch, angle, 0);
    }

    void Update()
    {
        currentPitch = currentPitch + Input.GetAxis("VerticalXbox") * pitchSpeed;
        currentPitch = Mathf.Clamp(currentPitch, minPitch, maxPitch);

        float yaw = transform.eulerAngles.y + Input.GetAxis("HorizontalXbox") * yawSpeed;

        transform.rotation = Quaternion.Euler(currentPitch, yaw, 0);
    }

}
//// Update is called once per frame
//void Update()
//{
//    float speed = 3.0f;
//    float xRot = speed * Input.GetAxis("VerticalXbox");
//    float yRot = speed * Input.GetAxis("HorizontalXbox");
//    transform.Rotate(xRot, yRot, 0.0f);
//    transform.eulerAngles.x = Mathf.Clamp(transform.eulerAngles.x, 0, 90);
//}
