using Alligator;
using Canoe;
using UnityEngine;

namespace Obstacles
{
    public class LilypadObstacle : Obstacle
    {
        [SerializeField] private int _aliSlowAmount;
    
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
