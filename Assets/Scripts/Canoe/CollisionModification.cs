using UnityEngine;

namespace Canoe
{
    public class CollisionModification : MonoBehaviour
    {
        private void Awake()
        {
            Physics.IgnoreLayerCollision(3, 3);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Debug.Log(collision.gameObject.name);
        }
    }
}
