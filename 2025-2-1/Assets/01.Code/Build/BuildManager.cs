using System.Collections.Generic;
using _01.Code.Tower.Tower;
using DG.Tweening;
using Settings.InputSettings;
using UnityEngine;

public class BuildManager : MonoSingleton<BuildManager>
{
    [SerializeField] private InputReaderSO inputReader;
    [SerializeField] private List<GameObject> towerPrefabs;


    public void BuildTower(PlaceTile placeTile, TowerType type)
    {
        if (placeTile.CanBuild)
        {
            Tower t = Instantiate(towerPrefabs[(int)type]).GetComponent<Tower>();
            placeTile.SetTower(t);
            t.transform.position = placeTile.PlaceTrm.position + new Vector3(0, 15f, 0);
            t.transform.DOMoveY(placeTile.PlaceTrm.position.y, 1f).SetEase(Ease.InOutCubic);
        }
    }
}
