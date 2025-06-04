using System;
using System.Xml.Schema;
using _01.Code.Managers;
using _01.Code.Tower.Towers;
using Core.GameEvent;
using Settings.InputSettings;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _01.Code.Tower
{
    public class TowerDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private InputReaderSO inputReader;
        [SerializeField] private TowerType type;
        
        [SerializeField] private GameObject preview;
        [SerializeField] private GameObject Visual;
        [SerializeField] private TextMeshProUGUI priceText;

        [SerializeField] private int necessaryGold;

        [SerializeField] private GameEventChannelSO goldChannel;


        private Color _enableColor = new Color(1, 1, 1, 1);
        private Color _disableColor = new Color(1, 1, 1, 0.4f);
        
        private void Start()
        {
            goldChannel.AddListener<GoldValueChangeEvent>(HandleGoldValueChange);
            priceText.text = $"${necessaryGold}";
        }


        private void OnDestroy()
        {
            goldChannel.RemoveListener<GoldValueChangeEvent>(HandleGoldValueChange);

        }

        private void HandleGoldValueChange(GoldValueChangeEvent evt)
        {
            if(GoldManager.Instance.CheckEnoughGold(necessaryGold))
                Visual.GetComponent<Image>().color = _enableColor;
            else
                Visual.GetComponent<Image>().color = _disableColor;
        }


        public void OnBeginDrag(PointerEventData eventData)
        {
            if(GoldManager.Instance.CheckEnoughGold(necessaryGold) == false) return;
            preview.SetActive(true);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if(GoldManager.Instance.CheckEnoughGold(necessaryGold) == false) return;
            preview.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if(GoldManager.Instance.CheckEnoughGold(necessaryGold) == false) return;
            Camera mainCam = Camera.main;
            Ray camRay = mainCam.ScreenPointToRay(inputReader.MousePosition);

            if (Physics.Raycast(camRay, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.TryGetComponent(out PlaceTile placeTile))
                {
                    BuildManager.Instance.BuildTower(placeTile,type,necessaryGold);
                }
            }
        
            preview.SetActive(false);
        }
    }
}