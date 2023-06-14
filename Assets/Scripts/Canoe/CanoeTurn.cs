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

        private enum PaddlingValue  {Positive, Negative, None }

        public float turnRate = 1f;
        [SerializeField] float _turnPower = 0;
        [SerializeField] float _turnTreshold = 0.5f;
        [Range (0,1)][SerializeField] float _velocityLossOnTurn = 0.95f;
        [Range(0, 1)][SerializeField] float _turnMomentum = 0.75f;
        [SerializeField] float baseTurnPower = 3f;
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _strengthModifier;
        [SerializeField] float _maxTurnSpeed = 5;
        public bool reverseTurn = true;

       // public bool motionTurnEnabled = false;

        private void Awake()
        {

        }

        private void FixedUpdate()
        {
            //complexMotionTurn();
            constantBasedMotionTurn();
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
        void constantBasedMotionTurn()
        {
            float leftPowerTotal = 0;
            float rightPowerTotal = 0;
            float turnPowerTotal = 0;
            bool left = false;
            bool right = false;

            if (_leftPaddle.IsPaddling())
            {
                //leftPowerTotal = -baseTurnPower;
                left = true;
                //if is moving
                if(_leftPaddle.GetThrust().z > 0)
                {
                    leftPowerTotal = baseTurnPower;
                }
                else if (_leftPaddle.GetThrust().z < 0)
                {
                    leftPowerTotal = -baseTurnPower;
                }
            }

            if (_rightPaddle.IsPaddling())
            {
                //rightPowerTotal = -baseTurnPower;
                right = true;
                //if is moving
                if (_rightPaddle.GetThrust().z > 0)
                {
                    rightPowerTotal = baseTurnPower;
                }
                else if (_rightPaddle.GetThrust().z < 0)
                {
                    rightPowerTotal = -baseTurnPower;
                }
            }



            turnPowerTotal = leftPowerTotal - rightPowerTotal;
            if (left!=right)
            {
                _turnPower = turnPowerTotal*turnRate;
            }
            else if(!(right && left)) _turnPower *= _turnMomentum;
            //Debug.Log(_turnPowerTotal);

            Quaternion deltaRot = Quaternion.Euler(0, _turnPower * Time.fixedDeltaTime, 0);
            _rb.MoveRotation(_rb.rotation * deltaRot);
        }
        void complexMotionTurn()
        {
            float strMod = _leftPaddle._strength / _strengthModifier;
            float leftPowerTotal = 0;
            float rightPowerTotal = 0;
            float turnPowerTotal = 0;

            PaddlingValue leftValue = PaddlingValue.None;
            PaddlingValue rightValue = PaddlingValue.None;

            if (_leftPaddle.IsPaddling() && _leftPaddle.GetPosZ() > -0.5) //&& _leftPaddle.GetDepth() < -0.2)
            {               
                leftPowerTotal += _leftPaddle.GetThrust().z / strMod;
                Debug.Log(leftPowerTotal);

                if (leftPowerTotal < 0) leftValue = PaddlingValue.Negative;
                else leftValue = PaddlingValue.Positive;
                leftPowerTotal -= baseTurnPower;

                if ((reverseTurn && leftValue == PaddlingValue.Negative) || leftValue == PaddlingValue.Positive)
                {
                    turnPowerTotal += leftPowerTotal;
                }
                //Debug.Log(_leftPaddle.GetThrust());
            }
            if (_rightPaddle.IsPaddling() && _rightPaddle.GetPosZ() > -0.5)
            {
                rightPowerTotal += _rightPaddle.GetThrust().z / strMod;
                if (rightPowerTotal < 0) rightValue = PaddlingValue.Negative;
                else rightValue = PaddlingValue.Positive;
                rightPowerTotal -= baseTurnPower;

                if ((reverseTurn && rightValue == PaddlingValue.Negative) || rightValue == PaddlingValue.Positive)
                {
                    turnPowerTotal -= rightPowerTotal;
                }
            }


            if (rightValue != leftValue || rightValue == PaddlingValue.None || leftValue == PaddlingValue.None)
            {
                
                if (Mathf.Abs(turnPowerTotal) > _turnTreshold)
                {
                    _turnPower = turnPowerTotal;
                }
                else _turnPower *= _turnMomentum;
                //Debug.Log(_turnPowerTotal);

                Quaternion deltaRot = Quaternion.Euler(0, _turnPower * turnRate * Time.fixedDeltaTime, 0);
                _rb.MoveRotation(_rb.rotation * deltaRot);

                if (_turnPower != 0) _rb.velocity *= _velocityLossOnTurn;
            }
            else Debug.Log("paddlin, not turnin");
            
        }

    }
}
