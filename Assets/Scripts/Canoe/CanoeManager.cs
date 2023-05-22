using UnityEngine;
using UnityEngine.Events;

namespace Canoe
{
    public class CanoeManager : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private HealthBar[] _healthBars;

        public UnityEvent loss;
        private float _timer;
        bool _isInvincible = false;
        [SerializeField] private float _invincibilityTimer = 0.5f;


        private void Start()
        {
            transform.position += new Vector3(0, 0.819999993f, 0);
        }

        private void Update()
        {
            if (_isInvincible)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0) _isInvincible = false;
            }
        }
        public void OnHit()
        {
            if (!_isInvincible)
            {
                _health--;
                foreach(HealthBar healthBar in _healthBars)
                {
                    healthBar.TakeDamage(1);
                }
             
                if (_health <= 0)
                {
                    loss?.Invoke();
                }
                _isInvincible = true;
                _timer = _invincibilityTimer;
            }
        }
        
    }
}
