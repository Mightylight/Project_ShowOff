using UnityEngine;

namespace Canoe
{
    public class CanoeMove : MonoBehaviour
    {
        public Paddle[] _paddles;
        [SerializeField] Rigidbody _rb;
        [SerializeField] float veloCap;
        [SerializeField] Vector3 current;
        [SerializeField] Vector3 vel = new Vector3(0, 0, 0); 

        Vector3 lastRotation;

        private void OnEnable ()
        {
           // _rb.AddForce(current);// paddles = GetComponentsInChildren<Paddle>();
        }
        private void Update()
        {
            //vel *= 0.95f;
            vel *= 0.00001f;//2;
        }
        private void FixedUpdate()
        {
            //_rb.velocity = vel;
            _rb.MovePosition(transform.position+current*Time.fixedDeltaTime);

            //Vector3 rotDiff = transform.rotation.eulerAngles - lastRotation;
            Vector3 velocity = Vector3.zero;
            foreach (Paddle p in _paddles)
            {
                velocity += p.GetThrust();
            }
            vel += transform.forward * velocity.magnitude;
            vel = Vector3.ClampMagnitude(vel, veloCap);
            
           //Debug.Log(vel + " and " +vel*0.00001f);//*0.95f);
            
            //Debug.Log(vel);
            _rb.AddForce(transform.forward * velocity.magnitude);
            _rb.velocity = Vector3.ClampMagnitude(_rb.velocity, veloCap);

        }


        private void OnCollisionEnter(Collision pCollision)
        {
            Debug.Log(pCollision.gameObject.name);
        }
    }
}
