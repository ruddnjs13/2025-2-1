using System.Collections.Generic;
using Code.Towers;
using UnityEngine;

namespace Code.Build
{
    public class PlaceTile : MonoBehaviour
    {
        private const int MaxTowerCount = 3;

        [field: SerializeField] public Transform[] PlaceTrm { get; private set; }
        [SerializeField] public List<TowerBase> ownTowerBase = new List<TowerBase>(MaxTowerCount);
        
        public bool CanBuild => ownTowerBase.Count < MaxTowerCount;

        public void SetTower(TowerBase tower)
        {
            if (!CanBuild) return;
            ownTowerBase.Add(tower);
            RefreshTowerPositions();
        }

        public void MergeTower(PlaceTile prevTile)
        {
            if (!CanBuild || prevTile == null) return;

            int availableSpace = MaxTowerCount - ownTowerBase.Count;
            int amountToAdd = Mathf.Min(availableSpace, prevTile.ownTowerBase.Count);

            for (int i = 0; i < amountToAdd; i++)
            {
                ownTowerBase.Add(prevTile.ownTowerBase[i]);
            }
            
            RefreshTowerPositions();
        }

        public void PutTower(PlaceTile prevTile)
        {
            if (!CanBuild || prevTile == null) return;

            ownTowerBase.AddRange(prevTile.ownTowerBase);
            RefreshTowerPositions();
        }

        public void SwapTower(PlaceTile otherTile)
        {
            if (otherTile == null) return;

            var temp = new List<TowerBase>(ownTowerBase);
            ownTowerBase = new List<TowerBase>(otherTile.ownTowerBase);
            otherTile.ownTowerBase = temp;

            RefreshTowerPositions();
            otherTile.RefreshTowerPositions();
        }

        public void RefreshTowerPositions()
        {
            for (int i = 0; i < ownTowerBase.Count; i++)
            {
                if (i < PlaceTrm.Length)
                    ownTowerBase[i].transform.position = PlaceTrm[i].position;
            }
        }

        public void CancelMoveTower() => RefreshTowerPositions();

        public void ClearTower() => ownTowerBase.Clear();
    }
}