using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPivot : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float speed = 3.0f;
        float xRot = speed * Input.GetAxis("VerticalXbox");
        float yRot = speed * Input.GetAxis("HorizontalXbox");
        transform.Rotate(0, yRot, 0.0f);
    }
}
