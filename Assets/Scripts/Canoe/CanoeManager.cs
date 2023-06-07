using UnityEngine;
using UnityEngine.Events;
using TMPro;

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
        [SerializeField] TextMeshProUGUI timerText;

        [SerializeField] private AudioClip _boatDamage;
        private AudioSource _audio;


        private void Start()
        {
           // transform.position += new Vector3(0, 0.35f, 0);
           _audio.GetComponent<AudioSource>();
        }

        private void Update()
        {
            if (_isInvincible)
            {
                _timer -= Time.deltaTime;
                //timerText.text = "Next bite in " + _timer.ToString();
                if (_timer <= 0) 
                {
                    _isInvincible = false;
                    //timerText.text = "Bite ready.";
                } 
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
                    _audio.PlayOneShot(_boatDamage, 0.5f);


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
