using UnityEngine;
using UnityEngine.Events;

namespace Canoe
{
    public class CanoeManager : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private HealthBar _healthBar;

        public UnityEvent loss;


        public void OnAlligatorHit()
        {
            _health--;
            _healthBar.TakeDamage(1);

            if(_health <= 0)
            {
                //TODO: Loss
            }
        }
    }
}
