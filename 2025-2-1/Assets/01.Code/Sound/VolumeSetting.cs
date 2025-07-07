using System;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace _01.Code.Sound
{
    public class VolumeSetting : MonoBehaviour
    {
        [SerializeField] private AudioMixer mixer;
        
        [SerializeField] private Slider masterSlider;
        [SerializeField] private Slider bgmSlider;
        [SerializeField] private Slider sfxSlider;
        [SerializeField] private SliderDataSO sliderData;

        bool isInit = false;
        private void Start()
        {
            LoadVolumeSettings();
            InitSlider();
        }

        public void InitSlider()
        {
            masterSlider.value = PlayerPrefs.GetFloat("MasterVolume", sliderData.MasterSoundScale);
            bgmSlider.value = PlayerPrefs.GetFloat("BgmVolume", sliderData.BGMSoundScale);
            sfxSlider.value = PlayerPrefs.GetFloat("SfxVolume", sliderData.SFXSoundScale);

            SetMasterVolume(masterSlider.value);
            SetBgmVolume(bgmSlider.value);
            SetSfxVolume(sfxSlider.value);

            isInit = true;
        }

        public void SetMasterVolume(float value)
        {
            if (!isInit) return;
            mixer.SetFloat("Master", Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat("MasterVolume", value);
        }

        public void SetBgmVolume(float value)
        {
            if (!isInit) return;
            mixer.SetFloat("Music", Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat("BgmVolume", value);
        }

        public void SetSfxVolume(float value)
        {
            if (!isInit) return;
            mixer.SetFloat("Sfx", Mathf.Log10(value) * 20);
            PlayerPrefs.SetFloat("SfxVolume", value);
        }

        private void LoadVolumeSettings()
        {
            sliderData.MasterSoundScale = PlayerPrefs.GetFloat("MasterVolume", sliderData.MasterSoundScale);
            sliderData.BGMSoundScale = PlayerPrefs.GetFloat("BgmVolume", sliderData.BGMSoundScale);
            sliderData.SFXSoundScale = PlayerPrefs.GetFloat("SfxVolume", sliderData.SFXSoundScale);
        }

    }
}