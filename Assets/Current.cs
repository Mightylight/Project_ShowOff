using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Current : MonoBehaviour
{
    [SerializeField] public Rigidbody _rb;
    [SerializeField] Vector3 _current = new Vector3(0, 0, 2);
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (_rb == null) return;
        _rb.MovePosition(transform.position + _current * Time.fixedDeltaTime);
    }
}
