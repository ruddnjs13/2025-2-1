using System.Collections;
using System.Collections.Generic;
using _01.Code.Combat.Projectiles;
using _01.Code.Enemies;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace _01.Code.Tower.Towers
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