using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using TMPro;

namespace Canoe
{
    public class CanoeManager : MonoBehaviour
    {
        [SerializeField] private CanoeMove _canoeMove;
        
        
        [SerializeField] private int _health;
        [SerializeField] private HealthBar[] _healthBars;

        public UnityEvent end;
        public UnityEvent win;
        public UnityEvent loss;

        [SerializeField] private float _invincibilityTimer = 0.5f;
        [SerializeField] TextMeshProUGUI timerText;
        
        [SerializeField] private AudioClip _boatDamage;
        
        private AudioSource _audio;
        private float _timer;
        bool _isInvincible = false;


        private void Start()
        {
           // transform.position += new Vector3(0, 0.35f, 0);
           _audio = GetComponent<AudioSource>();
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
                    end?.Invoke();
                    loss?.Invoke();
                }
                _isInvincible = true;
                _timer = _invincibilityTimer;
            }
        }

        public void Slow()
        {
            float originalMass = _canoeMove._rb.mass;
           _canoeMove._rb.mass = 100;
           StartCoroutine(DisableSlow(originalMass));
        }

        private IEnumerator DisableSlow(float pOriginalMass)
        {
            yield return new WaitForSeconds(2f);
            _canoeMove._rb.mass = pOriginalMass;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("PlayerGoal"))
            {
                end?.Invoke();
                win?.Invoke();
            }
        }
    }
}
