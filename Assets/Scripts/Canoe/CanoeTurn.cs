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
        [SerializeField] float _turnPowerTotal = 0;
        [SerializeField] float _turnTreshold = 0.5f;
        [Range (0,1)][SerializeField] float _velocityLossOnTurn = 0.95f;
        [Range(0, 1)][SerializeField] float _turnMomentum = 0.75f;
        [SerializeField] float baseTurnPower = 0.5f;
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

            PaddlingValue leftValue = PaddlingValue.None;
            PaddlingValue rightValue = PaddlingValue.None;

            if (_leftPaddle.IsPaddling() && _leftPaddle.GetPosZ() > -0.5) //&& _leftPaddle.GetDepth() < -0.2)
            {               
                leftPowerTotal -= _leftPaddle.GetThrust().z / strMod;

                if (leftPowerTotal < 0) leftValue = PaddlingValue.Positive;
                else leftValue = PaddlingValue.Negative;

                if ((reverseTurn && leftValue == PaddlingValue.Negative) || leftValue == PaddlingValue.Positive)
                {
                    leftPowerTotal += baseTurnPower;
                }
                //Debug.Log(_leftPaddle.GetThrust());
            }
            if (_rightPaddle.IsPaddling() && _rightPaddle.GetPosZ() > -0.5)
            {
                rightPowerTotal -= _rightPaddle.GetThrust().z / strMod;
                if (rightPowerTotal < 0) rightValue = PaddlingValue.Positive;
                else rightValue = PaddlingValue.Negative;

                if((reverseTurn && rightValue == PaddlingValue.Negative) || rightValue == PaddlingValue.Positive)
                {
                    rightPowerTotal += baseTurnPower;
                }
            }

            
            float turnPowerTotal = rightPowerTotal - leftPowerTotal;

            if (rightValue != leftValue || rightValue == PaddlingValue.None || leftValue == PaddlingValue.None)
            {
                
                if (Mathf.Abs(turnPowerTotal) > _turnTreshold)
                {
                    _turnPowerTotal = turnPowerTotal;
                }
                else _turnPowerTotal *= _turnMomentum;
                //Debug.Log(_turnPowerTotal);

                Quaternion deltaRot = Quaternion.Euler(0, _turnPowerTotal * turnRate * Time.fixedDeltaTime, 0);
                _rb.MoveRotation(_rb.rotation * deltaRot);

                if (_turnPowerTotal != 0) _rb.velocity *= _velocityLossOnTurn;
            }
            else Debug.Log("paddlin, not turnin");
            
        }
    }
}
