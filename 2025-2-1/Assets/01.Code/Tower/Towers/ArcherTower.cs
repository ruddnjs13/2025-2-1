using System.Collections.Generic;
using _01.Code.Combat.Projectiles;
using _01.Code.Enemies;

namespace _01.Code.Tower.Towers
{
    public class ArcherTower : TowerBase
    {
        
        public override void Attack(Enemy target)
        {
            base.Attack(target);
            
            
            Arrow arrow = poolManager.Pop(projectileType) as Arrow;
            arrow.transform.position = firePos.position;
            
            arrow.SetTargetAndFire(target, damage);
        }
    }
}