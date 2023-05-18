using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

namespace Canoe
{
    public class CanoeTurn : MonoBehaviour
    {
        public SteamVR_Action_Vector2 _leftInput;
        public SteamVR_Action_Vector2 _rightInput;

        public Paddle _leftPaddle;
        public Paddle _rightPaddle;


        //public SteamVR_Action_Boolean leftDraw;
        private bool _isMoving = false;

        private readonly List<Vector3> _positionList= new List<Vector3>();

        public float turnRate = 100f;
        [SerializeField] private Rigidbody _rb;

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
            Quaternion deltaRot = Quaternion.Euler(0, (_leftInput.axis.y - _rightInput.axis.y) * turnRate * Time.fixedDeltaTime, 0);
            _rb.MoveRotation(_rb.rotation * deltaRot);
        }

        private void OnTriggerEnter(Collider other)
        {
        
        }

        void motionTurn()
        {
            float turnPowerTotal = 0;
            float leftPowerTotal = 0;
            float rightPowerTotal = 0;
            if (_leftPaddle.IsPaddling())
            {
                leftPowerTotal += 1f;
            }
            if (_rightPaddle.IsPaddling())
            {
                rightPowerTotal+= 1f;
            }

            turnPowerTotal = rightPowerTotal - leftPowerTotal;



        
            Quaternion deltaRot = Quaternion.Euler(0, turnPowerTotal*turnRate*Time.fixedDeltaTime, 0);
            _rb.velocity = deltaRot * _rb.velocity;
            _rb.MoveRotation(_rb.rotation * deltaRot);


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
            _isMoving = true;
            _positionList.Clear();
        }

        void EndMovement()
        {
            Debug.Log("end move");
            _isMoving = false;
        }

        void UpdateMovement()
        {
            Debug.Log("update move");
        }
    }
}
