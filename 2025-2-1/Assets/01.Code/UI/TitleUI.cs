using System;
using Code.Core;
using Code.Feedback;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Code.UI
{
    [Serializable]
    public enum BtnType
    {
        None,
        Start,
        Setting,
        Exit,
        Quit,
    }

    public class TitleUI : MonoSingleton<TitleUI>
    {
        [SerializeField] private SettingUI _settingUI;
        [SerializeField] private GameObject _titleUI;
        
        private BtnType _btnType = BtnType.None;
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
                    SceneManager.LoadScene(1);
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
}