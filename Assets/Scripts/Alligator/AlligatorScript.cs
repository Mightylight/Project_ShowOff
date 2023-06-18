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
        [SerializeField] private float _pushBack = 0;
        private float _pushTimer = 0;

        [SerializeField] private Vector3 _pushDirection= Vector3.zero;
        
        [SerializeField] private CanoeManager _canoe;
        [SerializeField] private ControllerMovement _controllerMovement;
        [SerializeField] private float _biteInterval = 0.5f;
        [SerializeField] private Transform _parent;

        [SerializeField] private AudioClip _bonkSound;
        [SerializeField] private Current _current;
        
        private AudioSource _audio;
        
        
        private JoystickControls _joystickControls;
        

        private float _timer;
        private float _biteTimer;
        bool _isInvincible = false;
        private bool _isAttached = false;
        private bool _beingPushedBack = false;
        [SerializeField] private float _pushTime;

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
            
            if(Input.GetKeyDown(KeyCode.A))
            {
                OnHit();
            }

            if(_beingPushedBack)
            {
                Debug.Log("bruh");
                
                _pushTimer -= Time.deltaTime;
                if (_pushTimer <= 0)
                {
                    _beingPushedBack = false;
                    _controllerMovement.EnableMovement();
                }
                else _parent.Translate(_pushDirection);              

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
                
                
                
                _pushDirection = -transform.forward * _pushBack;
      
                
                _audio.PlayOneShot(_bonkSound, 0.5f);

                if (_rb == null)
                {
                _isAttached = false;
                //_controllerMovement.EnableMovement();
                transform.SetParent(_parent);
                _rb = gameObject.AddComponent<Rigidbody>();
                _rb.isKinematic = true;
                RigidbodyConstraints constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ | RigidbodyConstraints.FreezePositionY;
                _rb.constraints = constraints;
                _rb.useGravity = false;
                _current._rb = _rb;
                }
               

                if (_rb != null)
                {
                    _beingPushedBack = true;
                    _pushTimer = _pushTime; //transform.parent.Translate(_pushDirection);
                }

                if (_health <= 0)
                {
                    loss?.Invoke();
                }
            }
            
        }


        public void Slow(int pSlowAmount)
        {
            _controllerMovement._speed -= pSlowAmount;
        }

        private void Bite()
        {
            if (_isInRange && !_isAttached)
            {
                _controllerMovement.DisableMovement();
                _canoe.OnHit();
                _isAttached = true;
                if(_rb != null) Destroy(GetComponent<Rigidbody>());
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
                _rb.velocity= Vector3.zero;
                _rb.angularVelocity= Vector3.zero;
                _isInRange = false;
                
            }
        }
    }
}
