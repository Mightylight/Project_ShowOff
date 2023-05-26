using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    [SerializeField] float upperXRotClamp;
    [SerializeField] float lowerXRotClamp;
    // Start is called before the first frame update
    void Start()
    {
        
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

    float currentRotation;

    void SetCurrentRotation(float rot)
    {
        currentRotation = Mathf.Clamp(rot, -90, 90);
        transform.rotation = Quaternion.Euler(rot, 0, 0);
    }

    void Update()
    {
        float newRotation = currentRotation + Input.GetAxis("VerticalXbox") * 3;
        SetCurrentRotation(newRotation);
    }

}

