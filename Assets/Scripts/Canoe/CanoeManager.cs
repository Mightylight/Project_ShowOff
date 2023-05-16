using UnityEngine;

namespace Canoe
{
    public class CanoeManager : MonoBehaviour
    {
        [SerializeField] private int _health;


        public void OnAlligatorHit()
        {
            _health--;
        }
    }
}
