using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ControllerConnectionScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            string[] joysticks = Input.GetJoystickNames();
            foreach (string joystick in joysticks)
            {
                Debug.Log(joystick);
            }
        }
    }
}
