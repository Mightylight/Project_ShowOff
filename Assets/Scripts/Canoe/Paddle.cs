using Alligator;
using UnityEngine;

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
       // [SerializeField] private float _depthModifier = 0.5f;
       // [Range(-10, 0)][SerializeField] private float _maxDepthForStrength = -1;
        //private float paddlingTime = 0f;

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
                _lastFramePaddling = false; //last frame not paddling
            } 
        }

        void Paddling()
        {
            
            _lastPosition = _currentPosition;
            _currentPosition = _paddleTip.localPosition; //translate to the
            if(_lastFramePaddling) _thrust = -_strength * (_lastPosition - _currentPosition); //ignore if last frame not paddling
            _thrust.y = 0f;
            _lastFramePaddling = true;
            Debug.Log(_thrust.ToString());
        }

        private void OnTriggerEnter(Collider pOther)
        {
            if (pOther.CompareTag("paddleZone"))
            {
                Debug.Log("paddlin");
                _paddling= true;

                /*
                var paddleNum = Random.Range(1,4);
                AudioManager.instance.Play("Paddle " + paddleNum);
                */

            }
        }

        private void OnTriggerExit(Collider pOther)
        {
            if (pOther.CompareTag("paddleZone"))
            {
                Debug.Log("nope to paddlin");
                _paddling = false;
            }
        }

        public Vector3 GetThrust()
        {
            //Debug.Log(thrust.ToString());
            return _thrust;
        }
        public bool IsPaddling() { return _paddling; }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("alligator"))
            {
                collision.gameObject.GetComponent<AlligatorScript>().OnHit();
            }
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
