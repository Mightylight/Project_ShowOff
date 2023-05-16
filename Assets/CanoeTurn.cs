using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class CanoeTurn : MonoBehaviour
{
    public SteamVR_Action_Vector2 leftInput;
    public SteamVR_Action_Vector2 rightInput;

    public Paddle leftPaddle;
    public Paddle rightPaddle;


    //public SteamVR_Action_Boolean leftDraw;
    bool isMoving = false;

    List<Vector3> positionList= new List<Vector3>();

    public float turnRate = 100f;
    [SerializeField] Rigidbody rb;

    public bool motionTurnEnabled = false;

    private void Awake()
    {
        
    }

    private void FixedUpdate()
    {
        if (motionTurnEnabled) motionTurn();
        else simpleTurn();
    }

    void simpleTurn()
    {
        Quaternion deltaRot = Quaternion.Euler(0, (leftInput.axis.y - rightInput.axis.y) * turnRate * Time.fixedDeltaTime, 0);
        rb.MoveRotation(rb.rotation * deltaRot);
    }

    private void OnTriggerEnter(Collider other)
    {
        
    }

    void motionTurn()
    {
        float turnPowerTotal = 0;
        float leftPowerTotal = 0;
        float rightPowerTotal = 0;
        if (leftPaddle.IsPaddling())
        {
            leftPowerTotal += 1f;
        }
        if (rightPaddle.IsPaddling())
        {
            rightPowerTotal+= 1f;
        }

        turnPowerTotal = leftPowerTotal - rightPowerTotal;

        
        Quaternion deltaRot = Quaternion.Euler(0, turnPowerTotal*turnRate*Time.fixedDeltaTime, 0);
        rb.MoveRotation(rb.rotation * deltaRot);

        //Debug.Log(deltaRot.ToString());


        //if (!isMoving && leftDraw.state == true)
        //{
        //    StartMovement();
        //}
        //else if (isMoving && leftDraw.state == true)
        //{
        //    UpdateMovement();
        //}
        //else if (isMoving && leftDraw.state == false)
        //{
        //    EndMovement();
        //}
    }

    void StartMovement()
    {
        Debug.Log("start move");
        isMoving = true;
        positionList.Clear();
    }

    void EndMovement()
    {
        Debug.Log("end move");
        isMoving = false;
    }

    void UpdateMovement()
    {
        Debug.Log("update move");
    }
}
