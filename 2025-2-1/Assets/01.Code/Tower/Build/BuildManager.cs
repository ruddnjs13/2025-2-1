using System;
using System.Collections.Generic;
using _01.Code.Tower.Tower;
using Settings.InputSettings;
using UnityEngine;

public class BuildManager : MonoBehaviour
{
    [SerializeField] private InputReaderSO inputReader;
    
    private List<Tower> towerList;

    private void OnEnable()
    {
        inputReader.OnSelectEvent += HandleSelect;
    }

    private void HandleSelect()
    {
        
    }
}
