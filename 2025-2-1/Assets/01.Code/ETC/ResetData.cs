using _01.Code.Tower;
using UnityEngine;

namespace _01.Code.ETC
{
    public class ResetData : MonoBehaviour
    {
        [SerializeField] private towerDataSO[] towerData;

        public void Reset()
        {
            foreach (towerDataSO data in towerData)
            {
                data.ResetData();
            }
        }
    }
}