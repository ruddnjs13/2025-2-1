using System;
using TMPro;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace _01.Code.UI
{
    public class StageManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI headerText;
        
        [SerializeField] private CinemachineCamera[] cameras;
        [SerializeField] private Material[] skybox;
        [SerializeField] private string[] headerTexts = {"Forest", "SnowFall", "SkyGarden"};
        
        private readonly int maxStageIdx = 2;
        private int stageIdx = 0;

        private CinemachineCamera currentCamera;

        private void Start()
        {
            currentCamera = cameras[stageIdx];
        }


        public void MoveStage(bool isRight)
        {
            if (isRight)
            {
                if(stageIdx >= maxStageIdx) return;
                stageIdx++;
            }
            else
            {
                if(stageIdx <= 0) return;
                stageIdx--;
            }
            currentCamera.Priority = 0;
            currentCamera = cameras[stageIdx];
            currentCamera.Priority = 10;
            RenderSettings.skybox = skybox[stageIdx];
            DynamicGI.UpdateEnvironment();
            headerText.text = headerTexts[stageIdx];
            
        }
        
        public void StartStage()
        {
            SceneManager.LoadScene(stageIdx+2);
        }

        public void QuitMainMenu()
        {
            SceneManager.LoadScene(0);
        }
    }
    
   
}