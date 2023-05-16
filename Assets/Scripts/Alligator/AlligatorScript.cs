using Canoe;
using UnityEngine;

namespace Alligator
{
    public class AlligatorScript : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private float _stamina;
    

        public void OnHit()
        {
            _health--;
            //TODO: extra effect? Push alligator back or something
        }

        private void OnCollisionEnter(Collision pCollision)
        {
            Debug.Log("collision" + pCollision.gameObject.name);
            if (pCollision.gameObject.CompareTag("Canoe"))
            {
                //TODO: call canoe hit
                Debug.Log("Hit the canoe!");
                pCollision.gameObject.GetComponentInChildren<CanoeManager>().OnAlligatorHit();
            }
        }
    }
}
