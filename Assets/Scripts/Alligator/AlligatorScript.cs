using Canoe;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

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
        [SerializeField] private Transform _parent;

        [SerializeField] private AudioClip _bonkSound;
        private AudioSource _audio;
        
        
        private JoystickControls _joystickControls;
        

        private float _timer;
        private float _biteTimer;
        bool _isInvincible = false;
        private bool _isAttached = false;
        

        Rigidbody _rb;
        
        
        public bool bounceBack = false;
        public UnityEvent loss;
        private bool _isInRange;

        private void Awake()
        {
            _joystickControls = new JoystickControls();
            _rb = GetComponent<Rigidbody>();
            
            _joystickControls.Alligator.Bite.performed += pCtx => Bite();
            _joystickControls.Alligator.Enable();

            _audio = GetComponent<AudioSource>();

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
            Debug.Log("Hit");
            
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
                _audio.PlayOneShot(_bonkSound, 0.5f);
                
                _isAttached = false;
                _controllerMovement.EnableMovement();
                transform.SetParent(_parent);

                if(_health <= 0)
                {
                    loss?.Invoke();
                }
            }
            
        }

        private void Bite()
        {
            if (_isInRange)
            {
                _canoe.OnHit();
                _isAttached = true;
                _controllerMovement.DisableMovement();
                transform.SetParent(_canoe.transform);
            }
        }

        private void OnCollisionEnter(Collision pCollision)
        {
            Debug.Log(pCollision.gameObject.name);
            if (pCollision.gameObject.CompareTag("Canoe"))
            {
                Debug.Log("Hey");
                _isInRange = true;
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
                _isInRange = false;
                
            }
        }
    }
}
