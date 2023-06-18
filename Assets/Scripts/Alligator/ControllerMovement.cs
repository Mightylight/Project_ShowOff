using UnityEngine;
using UnityEngine.EventSystems;

//using UnityEngine.InputSystem.XR;

namespace Alligator
{
    public class ControllerMovement : MonoBehaviour
    {
        [SerializeField]private Rigidbody _rb;
        [SerializeField] private ControllerType _type = ControllerType.Xbox;
        [SerializeField] private CameraPivot _cameraPivot;
        
        
        [SerializeField] public float _speed = 5;
        private Vector3 _inputs = Vector3.zero;
        [SerializeField] Transform _model;
        float oldAngle = 0;
        private bool _isMoving = true;

        private void Awake()
        {
            _cameraPivot._type = _type;
            
        }

        private void Start()
        {
           // _rb = _model.GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if (!_isMoving) return;
            newMove();
        }

        private void FixedUpdate()
        {
           
            Vector3 current = new(0, 0, 2);
            //_rb.MovePosition(transform.position + current * Time.fixedDeltaTime);
            //moveRB();
        }

        void newMove()
        {
            _inputs.x = Input.GetAxis("Horizontal");
            _inputs.z = Input.GetAxis("Vertical");
           
            if (_inputs.magnitude >= 0.1f)
            {
                Vector3 movement = new Vector3(_inputs.x, 0.0f, _inputs.z);
                //movement *= transform.forward;
                float angle = Mathf.Atan2(_inputs.x, _inputs.z) * Mathf.Rad2Deg + _cameraPivot.transform.rotation.eulerAngles.y;
                _model.eulerAngles = new Vector3(0, angle, 0);
                movement = Quaternion.Euler(0, _cameraPivot.transform.rotation.eulerAngles.y, 0) * movement;
                transform.Translate(movement * _speed * Time.fixedDeltaTime);

            }
            else
            {
                //oldAngle = _model.eulerAngles.y;
            }

            
        }

        void moveRB()
        {
            _inputs.x = Input.GetAxis("Horizontal");
            _inputs.z = Input.GetAxis("Vertical");
            Vector3 current = new(0, 0, 2);
            
            //if (_inputs.magnitude >= 0.1f)
            //{
            //   // movement += new Vector3(_inputs.x, 0.0f, _inputs.z);
            //    //movement *= transform.forward;
            //    float angle = Mathf.Atan2(_inputs.x, _inputs.z) * Mathf.Rad2Deg + _cameraPivot.transform.rotation.eulerAngles.y;
            //    _model.eulerAngles = new Vector3(0, angle, 0);
            //   // movement = Quaternion.Euler(0, _cameraPivot.transform.rotation.eulerAngles.y, 0) * movement;

            //}
            //else
            //{
            //    //oldAngle = _model.eulerAngles.y;
            //}

            _rb.MovePosition(transform.position + current * Time.fixedDeltaTime );
        }

        void cameraMove()
        {

        }

        private void Move()
        {
            //_inputs
            Vector3 movement = new Vector3(_inputs.x, 0.0f, _inputs.z);
            transform.Translate(movement * _speed * Time.deltaTime);
        }

        private void Turn()
        {
            _inputs = Vector3.zero;

            if (_type is ControllerType.Ps)
            {
                _inputs.x = Input.GetAxis("HorizontalPS");
                _inputs.z = Input.GetAxis("VerticalPS");
            }
            else
            {
                _inputs.x = Input.GetAxis("HorizontalXbox");
                _inputs.z = Input.GetAxis("VerticalXbox");
            }

            if(_inputs.z < 0)
            {
                _inputs.z = 0;
            }

            if(_inputs != Vector3.zero)
            {
                transform.forward = _inputs;
            }


        }
        
        public void DisableMovement()
        {
            _isMoving = false;
        }

        public void EnableMovement()
        {
            _isMoving = true;
        }
        public enum ControllerType
        {
            Ps,Xbox
        }

        // private void Move()
        // {
        //     _rb.MovePosition(_rb.position + _inputs * _speed * Time.fixedDeltaTime);
        // }
    }
    
    
}
