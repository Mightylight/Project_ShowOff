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
            Turn();
        }

        private void FixedUpdate()
        {
            //Move();
            Move();
        }

        private void Move()
        {
            Vector3 movement = new Vector3(_inputs.x, 0.0f, Mathf.Abs(_inputs.z));
            
            transform.Translate(movement * _speed * Time.deltaTime);
        }

        private void Turn()
        {
            _inputs = Vector3.zero;
            _inputs.x = Input.GetAxis("Horizontal2");
            _inputs.z = Input.GetAxis("Vertical2");
            Debug.Log(_inputs);
        
         

            if(_inputs != Vector3.zero)
            {
                // transform.forward = _inputs;
                transform.rotation = Quaternion.LookRotation(_inputs);
            }


        }

        // private void Move()
        // {
        //     _rb.MovePosition(_rb.position + _inputs * _speed * Time.fixedDeltaTime);
        // }
    }
}
