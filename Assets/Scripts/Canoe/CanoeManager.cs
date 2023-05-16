using UnityEngine;

namespace Canoe
{
    public class CanoeManager : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private HealthBar _healthBar;


        public void OnAlligatorHit()
        {
            _health--;
            _healthBar.TakeDamage(1);
        }
    }
}
