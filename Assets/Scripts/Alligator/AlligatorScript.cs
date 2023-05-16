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
        private float _timer;
        bool _isInvincible = false;

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
            }
            
            
            //TODO: extra effect? Push alligator back or something
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
