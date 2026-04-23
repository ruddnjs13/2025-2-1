using _01.Code.Sound;
using Code.Feedback;
using RuddnjsPool;
using UnityEngine;

namespace Code.Sound
{
    public class SoundFeedback :MonoBehaviour, IFeedback
    {
        [SerializeField] private SoundSO _soundData;
        [SerializeField] private PoolManagerSO poolManager;
        [SerializeField] private PoolTypeSO soundPlayerType;

        public void PlayFeedback(Transform trm)
        {
            SoundPlayer soundPlayer =  poolManager.Pop(soundPlayerType) as SoundPlayer;
            soundPlayer.PlaySound(_soundData);
        }
    }
}
