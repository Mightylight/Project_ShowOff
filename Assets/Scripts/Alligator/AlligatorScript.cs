using System.Collections;
using System.Timers;
using Canoe;
using UnityEngine;
using UnityEngine.Events;

namespace Alligator
{
    public class AlligatorScript : MonoBehaviour
    {
        
        [SerializeField] private float _stamina;

        [SerializeField] private float _invincibilityTimer = 0.5f;
        [SerializeField] private float _pushBack = 0;
        private float _pushTimer = 0;

        [SerializeField] private Vector3 _pushDirection= Vector3.zero;

        [SerializeField] private Animator _animator;
        [SerializeField] private HealthBar _cooldown;
        
        
        
        [SerializeField] private CanoeManager _canoe;
        [SerializeField] private ControllerMovement _controllerMovement;
        [SerializeField] private float _biteCooldown = 20;
        [SerializeField] private float _biteInterval = 0.5f;
        [SerializeField] private Transform _parent;

        [SerializeField] private AudioClip _bonkSound;
        [SerializeField] private Current _current;

        [SerializeField] private ParticleSystem _biteEffect;
        
        //private AudioSource _audio;
        
        
        private JoystickControls _joystickControls;
        

        private float _timer;
        private float _biteTimer;
        bool _isInvincible = false;
        private bool _isAttached = false;
        private bool _beingPushedBack = false;

        private float _biteCooldownTimer;
        private bool _isBiteOnCooldown = false;



        [SerializeField] private float _pushTime;

        Rigidbody _rb;
        
        
        public bool bounceBack = false;
        public UnityEvent loss;
        private bool _isInRange;

        private Collider _collider;

        private void Awake()
        {
            _joystickControls = new JoystickControls();
            _rb = GetComponent<Rigidbody>();
            
            _joystickControls.Alligator.Bite.performed += pCtx => Bite();
            _joystickControls.Alligator.Enable();

            //_audio = GetComponent<AudioSource>();
            _collider = GetComponent<Collider>();

        }

        private void Update()
        {
            Timers();

            if(Input.GetKeyDown(KeyCode.A))
            {
                OnHit();
            }

            ChangeAnimationSpeed();

        }

        private void ChangeAnimationSpeed()
        {
            _animator.SetFloat("swimmingSpeedMultiplier",_controllerMovement.GetSpeed());
        }

        private void Timers()
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

            if (_isBiteOnCooldown)
            {
                _biteCooldownTimer -= Time.deltaTime;
                if (_biteCooldownTimer <= 0)
                {
                    _isBiteOnCooldown = false;
                    
                }
                _cooldown.SetHealth(_biteCooldownTimer,true);
            }
            
            if(_beingPushedBack)
            {                
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
            _biteEffect.Play();
        }   

        public void OnHit()
        {
            
            if (!_isInvincible)
            {
                
                _isInvincible = true;
                _timer = _invincibilityTimer;
                _animator.SetBool("isAttatched", false);
                _pushDirection = -transform.forward * _pushBack;      
                
                //_audio.PlayOneShot(_bonkSound, 0.5f);
                AudioManager.instance.Play("Aligator Bonk");

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

                _collider.isTrigger = false;

                if (_rb != null)
                {
                    _beingPushedBack = true;
                    _pushTimer = _pushTime; //transform.parent.Translate(_pushDirection);
                }

                _rb.transform.position = new Vector3 (_rb.transform.position.x, 0, _rb.transform.position.z);
            }
            
        }


        public void Slow(int pSlowAmount)
        {
            _controllerMovement._speed -= pSlowAmount;
            StartCoroutine(DisableSlow(pSlowAmount));
        }

        private IEnumerator DisableSlow(int pSlowAmount)
        {
            yield return new WaitForSeconds(5);
            _controllerMovement._speed += pSlowAmount;
        }

        private void Bite()
        {
            if (_isInRange && !_isAttached && !_isBiteOnCooldown)
            {
                _isBiteOnCooldown = true;
                _biteCooldownTimer = _biteCooldown;
                
                //Start animation
                _animator.SetBool("isAttatched", true);
                
                _collider.isTrigger = true;
                _controllerMovement.DisableMovement();
                _canoe.OnHit();
                _isAttached = true;
                if(_rb != null) Destroy(GetComponent<Rigidbody>());
                transform.SetParent(_canoe.transform);
                Transform bitePoint = _canoe.GetClosestBitePoint(transform);
                
                transform.position = bitePoint.position;
                transform.rotation = bitePoint.rotation;
            }
        }

        private void OnCollisionEnter(Collision pCollision)
        {
            //Debug.Log(pCollision.gameObject.name);
            if (!pCollision.gameObject.CompareTag("Canoe")) return;
            
            //Debug.Log("Hey");
            _isInRange = true;
        }
        private void OnCollisionExit(Collision pCollision)
        {
            if (!pCollision.gameObject.CompareTag("Canoe")) return;
            
            _rb.velocity= Vector3.zero;
            _rb.angularVelocity= Vector3.zero;
            _isInRange = false;
        }
    }
}
