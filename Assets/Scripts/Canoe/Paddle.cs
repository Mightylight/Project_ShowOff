using Alligator;
using Unity.VisualScripting;
using UnityEngine;
using Valve.VR;

namespace Canoe
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private Transform _paddleTip;
        [SerializeField] private Transform _heldBy;
        private bool _paddling = false;
        private bool _lastFramePaddling = false;
        private Vector3 _lastPosition = Vector3.zero;
        private Vector3 _currentPosition = Vector3.zero;
        private Vector3 _thrust;
        [SerializeField] public float _strength = 1000;
        [SerializeField] float _thrustThreshold = 0.1f;
        [SerializeField] float _waterLevel = 0;
        [SerializeField] private float _depthModifier = 2f;
        [Range(-10, 0)][SerializeField] private float _maxDepthForStrength = -1;
        //private float paddlingTime = 0f;
        [SerializeField] Valve.VR.InteractionSystem.Player _player;
        [SerializeField] bool _isLeft = false;
        [SerializeField] ushort _hapticPulseTimePaddle = 1;
        [SerializeField] ushort _hapticPulseTimeBonk = 5;

        private void Update()
        {
            //HeldBy();
        }

        private void FixedUpdate()
        {
            if(_paddling)
            {
                Paddling();
            }
            else
            {
                _thrust = Vector3.zero;
                _lastFramePaddling = false; //last frame not paddling
            } 
        }

        void Paddling()
        {
            
            _lastPosition = _currentPosition;
            _currentPosition = _paddleTip.localPosition; //translate to the
            if (_lastFramePaddling) 
            {
                _thrust = -_strength * (_currentPosition - _lastPosition);
                //Debug.Log(GetPosZ());
            }
            //else Debug.Log(_currentPosition.y);//ignore if last frame not paddling
            // Debug.Log(_thrust.y);

            float depth = 0;

           if(_currentPosition.y < _maxDepthForStrength)
           {
               depth = _maxDepthForStrength;
           }
           else depth = - _currentPosition.y;

            //modify by depth
            //_thrust *= _depthModifier * depth;

            _thrust.y = 0f;
            _lastFramePaddling = true;
            //Debug.Log(_thrust.ToString());
            
        }

        private void OnTriggerEnter(Collider pOther)
        {
            if (pOther.CompareTag("alligator"))
            {
                if (_isLeft) _player.leftHand.TriggerHapticPulse(_hapticPulseTimeBonk);
                else _player.rightHand.TriggerHapticPulse(_hapticPulseTimeBonk);
                Debug.Log("hit alligator");
                pOther.gameObject.GetComponent<AlligatorScript>().OnHit();
            }
            else if (pOther.CompareTag("paddleZone"))
            {
                //Debug.Log("paddlin");
                _paddling= true;
                //SteamVR

                
                var paddleNum = Random.Range(1,4);
                AudioManager.instance.Play("Paddle " + paddleNum);

            }            
        }

        private void OnTriggerStay(Collider pOther)
        {
            if (pOther.CompareTag("paddleZone"))
            {
                if (_isLeft) _player.leftHand.TriggerHapticPulse(_hapticPulseTimePaddle);
                else _player.rightHand.TriggerHapticPulse(_hapticPulseTimePaddle);
            }

        }
        private void OnTriggerExit(Collider pOther)
        {
            if (pOther.CompareTag("paddleZone"))
            {
                //Debug.Log("nope to paddlin");
                _paddling = false;
            }
        }

        public Vector3 GetThrust()
        {
            //Debug.Log(thrust.ToString());
            if (_thrust.magnitude > _thrustThreshold) return _thrust;
            else return Vector3.zero;
        }

        public float GetPosZ()
        {
            return _currentPosition.z;
        }
        public float GetDepth()
        {
            return _currentPosition.y;
        }
        public bool IsPaddling() { return _paddling; }

        private void OnCollisionEnter(Collision collision)
        {
            // if (collision.gameObject.CompareTag("alligator"))
            // {
            //     Debug.Log("hit alligator");
            //     collision.gameObject.GetComponent<AlligatorScript>().OnHit();
            // }
        }

        //private void HeldBy()
        //{
        //    transform.parent.position = _heldBy.position;
        //    transform.parent.rotation = _heldBy.rotation;
        //}
    }
}

//if(_paddling) 
//{
//    _lastPosition = _currentPosition;
//    _currentPosition = _paddleTip.position;
//    //_currentPosition = transform.root.rotation*_currentPosition;
//    //Debug.Log(lastPosition - currentPosition);
//    _thrust = -_strength * (_lastPosition - _currentPosition);

//    float forceFromDepth = _depthModifier *- _maxDepthForStrength;
//    if(_paddleTip.position.y < _maxDepthForStrength)
//    {
//        forceFromDepth= _depthModifier *-_paddleTip.position.y;
//    }

//   // _thrust *= forceFromDepth;
//    _thrust.y = 0;
//}
//else
//{
//    _lastPosition = Vector3.zero;
//    _currentPosition = Vector3.zero;
//}
