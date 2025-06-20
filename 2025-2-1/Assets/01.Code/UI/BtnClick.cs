using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Feedbacks;
using UnityEngine;
using UnityEngine.EventSystems;

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
