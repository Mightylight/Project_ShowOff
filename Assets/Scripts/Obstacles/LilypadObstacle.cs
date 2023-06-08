using Alligator;
using Canoe;
using UnityEngine;

namespace Obstacles
{
    public class LilypadObstacle : Obstacle
    {
        [SerializeField] private int _slowAmount;
    
        public override void OnAlligatorHit(AlligatorScript pAlligatorScript)
        {
            pAlligatorScript.Slow(_slowAmount);
        }

        public override void OnCanoeHit(CanoeManager pCanoeManager)
        {
       
        }
    }
}
