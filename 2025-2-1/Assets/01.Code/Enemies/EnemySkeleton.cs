using System.Collections;

namespace _01.Code.Enemies
{
    public class EnemySkeleton : Enemy
    {
        protected override IEnumerator DeadCoroutine()
        {
            return base.DeadCoroutine();
        }
    }
}