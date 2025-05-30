using UnityEngine;

namespace _01.Code.Enemy
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "SO/Enemy/Data", order = 0)]
    public class EnemyDataSO : ScriptableObject
    {
        public int maxHealth;
        public int moveSpeed;
        public int damage;
    }
}