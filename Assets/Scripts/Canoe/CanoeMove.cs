using UnityEngine;

namespace Canoe
{
    public class CanoeMove : MonoBehaviour
    {
        public Paddle[] _paddles;
        [SerializeField] Rigidbody _rb;
  

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

            //velocity

            _rb.AddForce(velocity);
        }


        private void OnCollisionEnter(Collision pCollision)
        {
            Debug.Log(pCollision.gameObject.name);
        }
    }
}
