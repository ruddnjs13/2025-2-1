using Code.Combat.Projectiles;
using Code.Enemies;
using UnityEngine;

namespace Code.Towers
{
    public class ArcherTower : TowerBase
    {
        public override void Attack(Enemy target)
        {
            base.Attack(target);

            if (poolManager.Pop(projectileType) is Arrow arrow)
            {
                arrow.transform.position = firePos.position;
                
                arrow.SetTargetAndFire(target, towerData.damage);
            }
            else
            {
                Debug.LogWarning($"{gameObject.name}: 풀에서 Arrow 컴포넌트를 찾을 수 없습니다!");
            }
        }
    }
}