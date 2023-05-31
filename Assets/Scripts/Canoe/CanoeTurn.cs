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


        public float turnRate = 1f;
        [Range (0,1)][SerializeField] float velocityLoss = 0.95f;
        [SerializeField] float baseTurnPower = 0.5f;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _strengthModifier;

       // public bool motionTurnEnabled = false;

        private void Awake()
        {

        }

        private void FixedUpdate()
        {
            complexMotionTurn();
        }

        void simpleTurn()
        {
            Quaternion deltaRot = Quaternion.Euler(0, (_leftInput.axis.y - _rightInput.axis.y) * turnRate * Time.fixedDeltaTime, 0);
            _rb.MoveRotation(_rb.rotation * deltaRot);

        }        
        void simpleMotionTurn()
        {
            float turnPowerTotal = 0;
            float leftPowerTotal = 0;
            float rightPowerTotal = 0;
            if (_leftPaddle.IsPaddling())
            {
                leftPowerTotal++;
            }
            if (_rightPaddle.IsPaddling())
            {
                rightPowerTotal++;
            }

            turnPowerTotal = rightPowerTotal - leftPowerTotal;

            Quaternion deltaRot = Quaternion.Euler(0, turnPowerTotal * turnRate * Time.fixedDeltaTime, 0);
            _rb.MoveRotation(_rb.rotation * deltaRot);

        }
        void complexMotionTurn()
        {
            float strMod = _leftPaddle._strength / _strengthModifier;
            float leftPowerTotal = 0;
            float rightPowerTotal = 0;
            if (_leftPaddle.IsPaddling())
            {
                leftPowerTotal += baseTurnPower;
                leftPowerTotal -= _leftPaddle.GetThrust().z / strMod;
                Debug.Log(_leftPaddle.GetThrust());
            }
            if (_rightPaddle.IsPaddling())
            {
                rightPowerTotal += baseTurnPower;
                rightPowerTotal -= _rightPaddle.GetThrust().z / strMod;
            }

            float turnPowerTotal = rightPowerTotal - leftPowerTotal;
            //Debug.Log(turnPowerTotal);

            Quaternion deltaRot = Quaternion.Euler(0, turnPowerTotal * turnRate * Time.fixedDeltaTime, 0);
            _rb.MoveRotation(_rb.rotation * deltaRot);

            if(turnPowerTotal != 0) _rb.velocity *= velocityLoss; 
        }
    }
}
