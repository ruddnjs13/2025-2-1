using Code.Combat.Projectiles;
using Code.Enemies;

namespace Code.Towers
{
    public class Mage2Tower : TowerBase
    {
        public override void Attack(Enemy target)
        {
            base.Attack(target);
            
            ExplosionBall energyBall = poolManager.Pop(projectileType) as ExplosionBall;
            energyBall.transform.position = firePos.position;
            
            energyBall.SetTargetAndFire(target, towerData.damage);
        }
    }
}