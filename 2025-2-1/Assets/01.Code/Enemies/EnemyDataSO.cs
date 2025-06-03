using UnityEngine;

namespace _01.Code.Enemies
{
    [CreateAssetMenu(fileName = "EnemyData", menuName = "SO/Enemy/Data", order = 0)]
    public class EnemyDataSO : ScriptableObject
    {
        public int maxHealth;
        public float moveSpeed;
        public int damage;
        public int getGold;
    }
}