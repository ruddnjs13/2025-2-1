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
            
            FireBall fireBall = poolManager.Pop(projectileType) as FireBall;
            fireBall.transform.position = firePos.position;
            
            fireBall.SetTargetAndFire(target, damage);
        }
    }
}