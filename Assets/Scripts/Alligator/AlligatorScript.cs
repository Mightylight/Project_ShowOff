using Canoe;
using UnityEngine;

namespace Alligator
{
    public class AlligatorScript : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private float _stamina;
        [SerializeField] private HealthBar _healthBar;

        [SerializeField] private float _invincibilityTimer = 0.5f;
        [SerializeField] private Vector3 _pushBack = Vector3.zero;

        private float _timer;
        bool _isInvincible = false;

        Rigidbody _rb;

        public bool bounceBack = false;

        private void Awake()
        {
            _rb = GetComponent<Rigidbody>();
        }

        private void Update()
        {
            if(_isInvincible)
            {
                _timer -= Time.deltaTime;
                if(_timer<= 0) _isInvincible = false;
            }
        }

        public void OnHit()
        {
            if (!_isInvincible)
            {
                _health--;
                _healthBar.TakeDamage(1);
                _isInvincible = true;
                _timer = _invincibilityTimer;
                _rb.velocity= Vector3.zero;
                
                _pushBack = transform.rotation* _pushBack;
                _rb.AddForce(_pushBack);
            }
            
        }

        private void OnCollisionEnter(Collision pCollision)
        {
            if (pCollision.gameObject.CompareTag("Canoe"))
            {
                
                //TODO: call canoe hit
                pCollision.gameObject.GetComponent<CanoeManager>().OnAlligatorHit();
            }
        }
        private void OnCollisionExit(Collision pCollision)
        {
            if (pCollision.gameObject.CompareTag("Canoe"))
            {
                if (!bounceBack)
                {
                    _rb.velocity = Vector3.zero;
                    _rb.angularVelocity = Vector3.zero;
                }
                
            }
        }
    }
}
