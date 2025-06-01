using _01.Code.Tower.Towers;
using Settings.InputSettings;
using UnityEngine;
using UnityEngine.EventSystems;

namespace _01.Code.Tower
{
    public class TowerDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeField] private InputReaderSO inputReader;
        [SerializeField] private TowerType type;
        
        [SerializeField] private GameObject preview;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            preview.SetActive(true);
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            preview.transform.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            Camera mainCam = Camera.main;
            Ray camRay = mainCam.ScreenPointToRay(inputReader.MousePosition);

            if (Physics.Raycast(camRay, out RaycastHit hit, Mathf.Infinity))
            {
                if (hit.collider.TryGetComponent(out PlaceTile placeTile))
                {
                    BuildManager.Instance.BuildTower(placeTile,type);
                }
            }
        
            preview.SetActive(false);
        }
    }
}