using System.Collections.Generic;
using _01.Code.Tower.Towers;
using Core.GameEvent;
using DG.Tweening;
using Settings.InputSettings;
using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{
    [SerializeField] private InputReaderSO inputReader;
    [SerializeField] private List<GameObject> towerPrefabs;

    [SerializeField] private GameEventChannelSO goldChannel;

    public void BuildTower(PlaceTile placeTile, TowerType type, int spendAmount)
    {
        if (placeTile.CanBuild)
        {
            goldChannel.RaiseEvent(GoldEvent.spendGolEvent.Initialize(spendAmount));
            TowerBase t = Instantiate(towerPrefabs[(int)type]).GetComponent<TowerBase>();
            placeTile.SetTower(t);
            t.transform.position = placeTile.PlaceTrm.position + new Vector3(0, 15f, 0);
            t.transform.DOMoveY(placeTile.PlaceTrm.position.y, 1f).SetEase(Ease.InOutCubic)
                .OnComplete(() =>
                {
                    t.EnableTower();
                });
        }
    }
}
