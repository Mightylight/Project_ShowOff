using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlligatorScript : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private float _stamina;
    

    public void OnHit()
    {
        _health--;
        //TODO: extra effect? Push alligator back or something
    }

    private void OnTriggerEnter(Collider pOther)
    {
        if (pOther.CompareTag("Canoe"))
        {
            //TODO: call canoe hit
            
        }
    }
}
