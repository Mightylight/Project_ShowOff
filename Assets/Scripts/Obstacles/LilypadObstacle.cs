using Alligator;
using Canoe;
using UnityEngine;

namespace Obstacles
{
    public class LilypadObstacle : Obstacle
    {
        [SerializeField] private int _aliSlowAmount;

        private void Start()
        {
            transform.position = new Vector3(transform.position.x, 0, transform.position.z);
        }

        public override void OnAlligatorHit(AlligatorScript pAlligatorScript)
        {
            pAlligatorScript.Slow(_aliSlowAmount);
        }

        public override void OnCanoeHit(CanoeManager pCanoeManager)
        {
            pCanoeManager.Slow();
        }
    }
}
