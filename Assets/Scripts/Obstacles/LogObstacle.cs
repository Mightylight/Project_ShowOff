using Alligator;
using Canoe;

namespace Obstacles
{
    public class LogObstacle : Obstacle
    {
        public override void OnAlligatorHit(AlligatorScript pAlligatorScript)
        {
            pAlligatorScript.OnHit();
        }

        public override void OnCanoeHit(CanoeManager pCanoeManager)
        {
            pCanoeManager.OnHit();
        }
    }
}
