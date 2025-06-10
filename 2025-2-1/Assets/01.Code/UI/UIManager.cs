using System;
using _01.Code.UI;
using DG.Tweening;
using Feedbacks;
using UnityEngine;
using UnityEngine.SceneManagement;

[Serializable]
public enum BtnType
{
    None,
    Start,
    Setting,
    Exit,
    Quit,
}

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private SettingUI _settingUI;
    [SerializeField] private GameObject _titleUI;
    BtnType _btnType = BtnType.None;
    
    private FeedbackPlayer _feedbackPlayer;

    private void Awake()
    {
        _feedbackPlayer = GetComponentInChildren<FeedbackPlayer>();
    }

    private void BtnClick()
    {
        switch (_btnType)
        {
            case BtnType.Start: 
                SceneManager.LoadScene(6);
                break;
            case BtnType.Setting:
                _settingUI.Setup(false);
                break;
            case BtnType.Exit:
                Application.Quit();
                break;
            case BtnType.Quit:
                _settingUI.Setup(true);
                break;
        }
    }

    public void SetBtnAndClick(BtnType btnType)
    {
        _btnType = btnType;
        BtnClick();
    }
}
