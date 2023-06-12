using UnityEngine;

namespace Canoe
{
    public class CanoeMove : MonoBehaviour
    {
        public Paddle[] _paddles;
        [SerializeField] Rigidbody _rb;
        [SerializeField] float veloCap;
        [SerializeField] Vector3 current;
        [SerializeField][Range(0, 1)] private float _friction;

        private void FixedUpdate()
        {
            //_rb.velocity = vel;
            _rb.MovePosition(transform.position+current*Time.fixedDeltaTime);

            
            Vector3 paddleForce = Vector3.zero;
            foreach (Paddle p in _paddles)
            {
                paddleForce += p.GetThrust();
                //Debug.Log(p.GetThrust());
            }
            
            //vel += transform.forward * velocity.magnitude;
            //vel = Vector3.ClampMagnitude(vel, veloCap);
            
           //Debug.Log(vel + " and " +vel*0.00001f);//*0.95f);
            
            //Debug.Log(vel);
            _rb.AddForce(transform.forward * paddleForce.z);
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, veloCap);
            if(paddleForce.z == 0) _rb.velocity *= _friction;
            Debug.DrawRay(transform.position, transform.forward * paddleForce.magnitude  * 100, Color.red);

        }


        private void OnCollisionEnter(Collision pCollision)
        {
            Debug.Log(pCollision.gameObject.name);
        }
    }
}
