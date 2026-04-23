using Code.Feedback;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Code.UI
{
    public class BtnClick : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
    {
        [SerializeField]
        public BtnType btnType;
    
        private Vector3 originScale;
    
        private FeedbackPlayer _feedbackPlayer;

        private void Start()
        {
            originScale = transform.localScale;
        }

        private void OnDestroy()
        {
            DOTween.Kill(this);
        }

        public void BtnClicked()
        {
            TitleUI.Instance.SetBtnAndClick(btnType);        
            transform.localScale = originScale;

        }

   
        public void OnPointerEnter(PointerEventData eventData)
        {
            transform.localScale = originScale * 1.2f;
        }


        public void OnPointerExit(PointerEventData eventData)
        {
            transform.localScale = originScale;
        }
    }
}
