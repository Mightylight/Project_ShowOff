using UnityEngine;

namespace Canoe
{
    public class CanoeMove : MonoBehaviour
    {
        public Paddle[] _paddles;
        [SerializeField] Rigidbody _rb;
        [SerializeField] Vector3 velocityCap;
        [SerializeField] float veloCap = 5f;
  

        private void Awake()
        {
            // paddles = GetComponentsInChildren<Paddle>();
        }

        private void FixedUpdate()
        {
            Vector3 velocity = Vector3.zero;
            foreach (Paddle p in _paddles)
            {
                velocity += p.GetThrust();
            }

            //velocity = transform.rotation * velocity;
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, veloCap);
            _rb.AddForce(transform.forward * velocity.z*2);
            //_rb.AddForce(velocity);
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, veloCap);
            //Mathf.Clamp(_rb.velocity.x, -velocityCap.x, velocityCap.x);
            //Mathf.Clamp(_rb.velocity.z, -velocityCap.z, velocityCap.z);
        }


        private void OnCollisionEnter(Collision pCollision)
        {
            Debug.Log(pCollision.gameObject.name);
        }
    }
}
