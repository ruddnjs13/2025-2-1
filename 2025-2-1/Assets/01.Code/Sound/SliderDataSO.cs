using UnityEngine;

namespace _01.Code.Sound
{
    [CreateAssetMenu(fileName = "SliderData", menuName = "SO/SliderData", order = 0)]
    public class SliderDataSO : ScriptableObject
    {
        public float MasterSoundScale;
        public float SFXSoundScale;
        public float BGMSoundScale;
    }
}