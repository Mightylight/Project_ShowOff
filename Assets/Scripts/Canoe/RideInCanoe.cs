using UnityEngine;

namespace Canoe
{
    public class RideInCanoe : MonoBehaviour
    {
        [SerializeField] private CanoeSync _canoeSync;
        [SerializeField] private Rigidbody _canoe;
        private Vector3 _offset;

        private void FixedUpdate()
        {
            //if (canoeSync.synced)
            {
                _offset = _canoeSync.transform.position;
                //Debug.Log(offset);
                transform.position = new Vector3(_canoe.transform.position.x, transform.position.y, _canoe.transform.position.z);
                transform.rotation = _canoe.transform.rotation;
            }

        }
    }
}
