using _01.Code.Combat.Projectiles;
using _01.Code.Enemies;
using UnityEngine;

namespace _01.Code.Tower.Towers
{
    public class MageTower : TowerBase
    {
        public override void Attack(Enemy target)
        {
            base.Attack(target);
            
            EnergyBall energyBall = poolManager.Pop(projectileType) as EnergyBall;
            energyBall.transform.position = firePos.position;
            
            energyBall.SetTargetAndFire(target, damage);
        }
    }
}