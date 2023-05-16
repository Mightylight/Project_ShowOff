using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

/// <summary>
/// An abstract class that can be inherited from to make custom effect for obstacles
/// </summary>

[RequireComponent(typeof(BoxCollider))]
public abstract class Obstacle : MonoBehaviour
{
    public abstract void OnAlligatorHit(AlligatorScript pAlligatorScript);
    public abstract void OnCanoeHit();

    private void OnTriggerEnter(Collider pOther)
    {
        if (pOther.CompareTag("Alligator"))
        {
            OnAlligatorHit(pOther.GetComponent<AlligatorScript>());
        } else if (pOther.CompareTag("Canoe"))
        {
            OnCanoeHit();
        }
    }
}
