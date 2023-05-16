using UnityEngine;

namespace Canoe
{
    public class PlaceholderCanoeMove : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rb;
        [SerializeField] private float _speed = 5;
        public Vector3 _move;
        private void Awake()
        {
       
        }
        private void FixedUpdate()
        {
            //Debug.Log(input.axis.x + ", " + input.axis.y);
            _move = _rb.position + new Vector3(transform.forward.x, 0, transform.forward.z) * _speed * Time.fixedDeltaTime;
            _rb.MovePosition(_move);
        }
    }
}
