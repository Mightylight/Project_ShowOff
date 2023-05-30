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

        private readonly List<Vector3> _positionList = new List<Vector3>();

        public float turnRate = 1f;
        [SerializeField] float velocityCoefficient = 0.9f;
        [SerializeField] private Rigidbody _rb;

        public bool motionTurnEnabled = false;

        float test;

        private void Awake()
        {

        }

        private void FixedUpdate()
        {
            //// if (motionTurnEnabled) motionTurn();
            // //else simpleTurn();

            // Vector3 temp =  (_leftPaddle.GetThrust() - _rightPaddle.GetThrust());

            // //temp /= 10;
            // Quaternion deltaRot =Quaternion.Euler(0, temp.z * turnRate * Time.fixedDeltaTime, 0) ;

            // Debug.Log(temp.z * turnRate * Time.fixedDeltaTime);

            // Vector3.ClampMagnitude(_rb.angularVelocity, 0);
            // _rb.MoveRotation(_rb.rotation * deltaRot);
            // temp = Vector3.zero;
            // //if (_leftPaddle.GetThrust().magnitude != 0) Debug.Log(temp);





            //if (_leftPaddle.IsPaddling())
            //{
            //    test += 0.01f;
            //    Quaternion deltaRot = Quaternion.Euler(0, test * turnRate * Time.fixedDeltaTime, 0);
            //    _rb.MoveRotation(_rb.rotation * deltaRot);
            //}
            //else test = 0;

            float leftPowerTotal = 0;
            float rightPowerTotal = 0;
            if (_leftPaddle.IsPaddling())
            {
                leftPowerTotal++;
                leftPowerTotal += _leftPaddle.GetThrust().z / _leftPaddle._strength;
            }
            if (_rightPaddle.IsPaddling())
            {
                rightPowerTotal++;
                rightPowerTotal += _rightPaddle.GetThrust().z / _leftPaddle._strength;
            }

            float turnPowerTotal = rightPowerTotal - leftPowerTotal;
            Debug.Log(turnPowerTotal);

            Quaternion deltaRot = Quaternion.Euler(0, turnPowerTotal * turnRate * Time.fixedDeltaTime, 0);
            _rb.MoveRotation(_rb.rotation * deltaRot);
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
                rightPowerTotal += 1f;
            }

            turnPowerTotal = rightPowerTotal - leftPowerTotal;




            Quaternion deltaRot = Quaternion.Euler(0, turnPowerTotal * turnRate * Time.fixedDeltaTime, 0);
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
