using System.Collections.Generic;
using UnityEngine;

namespace Canoe
{
    public class CanoeSync : MonoBehaviour 
    {
        [SerializeField] Transform _vrCam;
        [SerializeField] float _yLocation = -0.5f;
        public bool _synced = false;
        [SerializeField] float _timeLeft = 1.0f;

        [SerializeField]List<MonoBehaviour> toEnable = new();
        [SerializeField] GameObject _gator;

        private void Awake()
        {
            toEnable.Add( GetComponentInParent<CanoeTurn>());
            toEnable.Add( GetComponentInParent<CanoeMove>());
            toEnable.Add(_gator.GetComponent<Current>());
            //toEnable.Add( transform.parent.GetComponentInParent<Current>());
        }
        private void Update()
        {
            if (_timeLeft < 0)
            {
                _synced = true;

                foreach(MonoBehaviour behaviour in toEnable)
                {
                    behaviour.enabled = true;
                }
            }
            else _timeLeft -= Time.fixedDeltaTime;
            if (!_synced)
            {
                transform.localPosition = new Vector3(_vrCam.position.x, -0.3f, _vrCam.position.z);
                _vrCam.position = new Vector3(_vrCam.position.x, 1.2f, _vrCam.position.z);

            }
        }
    }
}
