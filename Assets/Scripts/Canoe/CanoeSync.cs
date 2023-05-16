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
            if(!_synced)
            {
                transform.localPosition = new Vector3(_vrCam.position.x, _vrCam.position.y + _yLocation, _vrCam.position.z);
                transform.localRotation = Quaternion.Euler(0, _vrCam.localEulerAngles.y, 0); //vrCam.localEulerAngles
                //transform.localRotation = vrCam.localRotation;

                //Quaternion.EulerAngles
            }
        
        }
        //{
        //    
        //    public bool synced = false;
        //    [SerializeField] float timeLeft = 1.0f;
        //    [SerializeField] GameObject canoe;
        //    private void Start()
        //    {

        //    }
        //    private void FixedUpdate()
        //    {
        //        timeLeft -= Time.fixedDeltaTime;
        //        if (timeLeft < 0) synced = true;
        //        if(!synced) canoe.transform.position = new Vector3(vrCam.position.x, vrCam.position.y - 0.7f, vrCam.position.z);

        //    }

        //    private void OnTriggerEnter(Collider other)
        //    {
        //       // if (other.CompareTag("player"))synced= true;
        //    }
    }
}
