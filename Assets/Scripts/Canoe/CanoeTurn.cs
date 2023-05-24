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
        [SerializeField] float velocityCoefficient = 0.9f;
        [SerializeField] private Rigidbody _rb;

        public bool motionTurnEnabled = false;

        float test;

        private void Awake()
        {
        
        }

        private void FixedUpdate()
        {
            if (motionTurnEnabled) motionTurn();
            //else simpleTurn();

            Vector3 temp = Quaternion.Euler(0, -transform.rotation.eulerAngles.y, 0) * _leftPaddle.GetThrust();

            if (_leftPaddle.GetThrust().magnitude != 0) Debug.Log(temp);

           

            

            //if (_leftPaddle.IsPaddling())
            //{
            //    test += 0.01f;
            //    Quaternion deltaRot = Quaternion.Euler(0, test * turnRate * Time.fixedDeltaTime, 0);
            //    _rb.MoveRotation(_rb.rotation * deltaRot);
            //}
            //else test = 0;
           
        }

        void simpleTurn()
        {
            Quaternion deltaRot = Quaternion.Euler(0, (_leftInput.axis.y - _rightInput.axis.y) * turnRate * Time.fixedDeltaTime, 0);
            _rb.MoveRotation(_rb.rotation * deltaRot);
        }

        private void OnTriggerEnter(Collider other)
        {
        
        }

        float turnPower()
        {

            return 0;
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
            //_rb.velocity = deltaRot * _rb.velocity*velocityCoefficient;
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
