using System;
using Alligator;
using Canoe;
using UnityEngine;

namespace Obstacles
{
    /// <summary>
    /// An abstract class that can be inherited from to make custom effect for obstacles
    /// </summary>

    [RequireComponent(typeof(BoxCollider))]
    public abstract class Obstacle : MonoBehaviour
    {
        [SerializeField] private float _speed;
        public abstract void OnAlligatorHit(AlligatorScript pAlligatorScript);
        public abstract void OnCanoeHit(CanoeManager pCanoeManager);

        private void Update()
        {
            transform.position += Vector3.back * _speed * Time.deltaTime;
        }

        private void OnTriggerEnter(Collider pOther)
        {
            if (pOther.CompareTag("alligator"))
            {
                OnAlligatorHit(pOther.GetComponent<AlligatorScript>());
            } else if (pOther.CompareTag("Canoe"))
            {
                OnCanoeHit(pOther.GetComponent<CanoeManager>());
            }
        }
    }
}
