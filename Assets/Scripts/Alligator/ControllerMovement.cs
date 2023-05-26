using UnityEngine;

//using UnityEngine.InputSystem.XR;

namespace Alligator
{
    public class ControllerMovement : MonoBehaviour
    {
        private Rigidbody _rb;
        [SerializeField] private float _speed = 5;
        private Vector3 _inputs = Vector3.zero;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            //if (Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) Debug.Log("Joystick1");
            //if (Input.GetAxis("HorizontalXbox") != 0 || Input.GetAxis("VerticalXbox") != 0) Debug.Log("Joystick2");
            //Debug.Log(Input.GetAxis("HorizontalPS") + ", " + Input.GetAxis("VerticalPS"));

            //Turn();
        }

        private void FixedUpdate()
        {
            newMove();
        }

        void newMove()
        {
            _inputs.x = Input.GetAxis("Horizontal");
            _inputs.z = Input.GetAxis("Vertical");
            Vector3 movement = new Vector3(_inputs.x, 0.0f, _inputs.z);
            transform.Translate(movement * _speed * Time.deltaTime);
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
            _inputs.x = Input.GetAxis("Horizontal2");
            _inputs.z = Input.GetAxis("Vertical2");
        
            if(_inputs.z < 0)
            {
                _inputs.z = 0;
            }

            if(_inputs != Vector3.zero)
            {
                transform.forward = _inputs;
            }


        }

        // private void Move()
        // {
        //     _rb.MovePosition(_rb.position + _inputs * _speed * Time.fixedDeltaTime);
        // }
    }
}
