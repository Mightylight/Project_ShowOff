using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class PaddleHold : MonoBehaviour
{
    Hand hand;
    [SerializeField] GameObject paddle;

    private void Start()
    {
        hand = GetComponent<Hand>();
        //hand.AttachObject(paddle, GrabTypes.Grip, Hand.AttachmentFlags.TurnOffGravity);
    }
}
