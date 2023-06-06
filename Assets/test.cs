using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    [SerializeField] Rigidbody _rb;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 current = new Vector3(0, 0, 2);
        _rb.MovePosition(transform.position + current * Time.fixedDeltaTime);
    }
}
