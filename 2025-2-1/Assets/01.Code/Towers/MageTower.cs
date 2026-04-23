using Code.Combat.Projectiles;
using Code.Enemies;
using Code.Towers;

namespace _01.Code.Tower.Towers
{
    public class MageTower : TowerBase
    {
        public override void Attack(Enemy target)
        {
            base.Attack(target);
            
            if (poolManager.Pop(projectileType) is EnergyBall energyBall)
            {
                energyBall.transform.position = firePos.position;
                energyBall.SetTargetAndFire(target, towerData.damage);
            }
        }
    }
}