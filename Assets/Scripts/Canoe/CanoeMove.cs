using UnityEngine;

namespace Canoe
{
    public class CanoeMove : MonoBehaviour
    {
        public Paddle[] _paddles;
        [SerializeField] Rigidbody _rb;
        [SerializeField] float veloCap;

        Vector3 lastRotation;

        private void Awake()
        {
            // paddles = GetComponentsInChildren<Paddle>();
        }

        private void FixedUpdate()
        {
            Vector3 rotDiff = transform.rotation.eulerAngles - lastRotation;
            Vector3 velocity = Vector3.zero;
            foreach (Paddle p in _paddles)
            {
                velocity += p.GetThrust();
            }

            _rb.AddForce(transform.forward * velocity.magnitude);
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, veloCap);


            //_rb.AddForce(velocity);

            // //velocity = transform.rotation * velocity;
            // //
            // 
            // //_rb.AddForce(velocity);
            //// _rb.velocity = Quaternion.Euler(rotDiff) * _rb.velocity;
            // _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, veloCap);
            // _rb.velocity = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
            // //Mathf.Clamp(_rb.velocity.x, -velocityCap.x, velocityCap.x);
            // //Mathf.Clamp(_rb.velocity.z, -velocityCap.z, velocityCap.z);

            // //_rb.velocity *= 0.95f;
            // lastRotation = transform.rotation.eulerAngles;
        }


        private void OnCollisionEnter(Collision pCollision)
        {
            Debug.Log(pCollision.gameObject.name);
        }
    }
}
