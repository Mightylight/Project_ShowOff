using Alligator;
using UnityEngine;

namespace Canoe
{
    public class Paddle : MonoBehaviour
    {
        [SerializeField] private Transform _paddleTip;
        private bool _paddling = false;
       // private Vector3 _lastPosition = Vector3.zero;
        private Vector3 _currentPosition = Vector3.zero;
        private Vector3 _thrust;
        [SerializeField] public float _strength = 1000;
        [SerializeField] private float _depthModifier = 0.5f;
        [Range(-10, 0)][SerializeField] private float _maxDepthForStrength = -1;
        private float paddlingTime = 0f;


        private void FixedUpdate()
        {
            if(_paddling) 
            {
                Vector3 lastPosition = _currentPosition;
                _currentPosition = _paddleTip.position;// transform.parent.localPosition;
                //_currentPosition = transform.root.rotation*_currentPosition;
                //Debug.Log(lastPosition - currentPosition);
                if(lastPosition != new Vector3(999, 9999, 99)) _thrust = _strength * (lastPosition - _currentPosition);
                
                //float forceFromDepth = _depthModifier *- _maxDepthForStrength;
                //if(_paddleTip.position.y < _maxDepthForStrength)
                //{
                //    forceFromDepth= _depthModifier *-_paddleTip.position.y;
                //}

               // _thrust *= forceFromDepth;
                _thrust.y = 0;
            }
            else
            {
                //_lastPosition = Vector3.zero;
                _currentPosition = new Vector3(999, 9999, 99);
            }
        }

        private void OnTriggerEnter(Collider pOther)
        {
            if (pOther.CompareTag("paddleZone"))
            {
                Debug.Log("paddlin");
                _paddling= true;
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
    }
}
