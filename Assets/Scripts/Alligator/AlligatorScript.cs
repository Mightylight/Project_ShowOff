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
            
            
            //TODO: extra effect? Push alligator back or something
            //A: pushing away alligator happens anyway, since paddles are kinematic
            //but its definitely possible to add custom force :D
        }

        private void OnTriggerEnter(Collider pOther)
        {
            if (pOther.CompareTag("Canoe"))
            {
                //TODO: call canoe hit
                pOther.GetComponent<CanoeManager>().OnAlligatorHit();
            }
        }
    }
}
