using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreWaterCollision : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Physics.IgnoreLayerCollision(3, 4);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
