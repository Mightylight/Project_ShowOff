using Canoe;
using UnityEngine;
using UnityEngine.Events;

namespace Alligator
{
    public class AlligatorScript : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private float _stamina;
        [SerializeField] private HealthBar[] _healthBars;

        [SerializeField] private float _invincibilityTimer = 0.5f;
        [SerializeField] private Vector3 _pushBack = Vector3.zero;
        
        [SerializeField] private CanoeManager _canoe;
        [SerializeField] private ControllerMovement _controllerMovement;
        [SerializeField] private float _biteInterval = 0.5f;

        private float _timer;
        private float _biteTimer;
        bool _isInvincible = false;
        private bool _isAttached = false;
        

        Rigidbody _rb;

        
        
        public bool bounceBack = false;
        public UnityEvent loss;

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
            if (_isAttached)
            {
                _biteTimer -= Time.deltaTime;
                if (_biteTimer <= 0)
                {
                    _biteTimer = _biteInterval;
                    BiteCanoe();
                }
            }
        }

        private void BiteCanoe()
        {
            _canoe.OnHit();
        }
        

        public void OnHit()
        {
            
            if (!_isInvincible)
            {
                //_health--;
                foreach (HealthBar healthBar in _healthBars)
                {
                   // healthBar.TakeDamage(1);
                }
                
                _isInvincible = true;
                _timer = _invincibilityTimer;
                _rb.velocity= Vector3.zero;
                
                _pushBack = transform.rotation* _pushBack;
                _rb.AddForce(_pushBack);
                
                _isAttached = false;
                _controllerMovement.EnableMovement();
                transform.SetParent(null);

                if(_health <= 0)
                {
                    loss?.Invoke();
                }
            }
            
        }

        private void OnCollisionEnter(Collision pCollision)
        {
            if (pCollision.gameObject.CompareTag("Canoe"))
            {
                
                //TODO: call canoe hit
                pCollision.gameObject.GetComponent<CanoeManager>().OnHit();
                _isAttached = true;
                _controllerMovement.DisableMovement();
                transform.SetParent(_canoe.transform);
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
