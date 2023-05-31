using UnityEngine;

namespace Canoe
{
    public class CanoeSync : MonoBehaviour 
    {
        [SerializeField] Transform _vrCam;
        [SerializeField] float _yLocation = -0.5f;
        public bool _synced = false;
        [SerializeField] float _timeLeft = 1.0f;
        private void Update()
        {
            if (_timeLeft < 0) _synced = true;
            else _timeLeft -= Time.fixedDeltaTime;
            if (!_synced)
            {
                transform.localPosition = new Vector3(_vrCam.position.x, -0.3f, _vrCam.position.z);
                _vrCam.position = new Vector3(_vrCam.position.x, 1.2f, _vrCam.position.z);

            }
        }
    }
}
