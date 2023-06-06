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

        }


        private void OnCollisionEnter(Collision pCollision)
        {
            Debug.Log(pCollision.gameObject.name);
        }
    }
}
