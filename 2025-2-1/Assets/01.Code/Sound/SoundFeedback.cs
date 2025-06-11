using System.Collections;
using System.Collections.Generic;
using Feedbacks;
using RuddnjsPool;
using UnityEngine;

public class SoundFeedback : IFeedback
{
    [SerializeField] private SoundSO _soundData;
    [SerializeField] private PoolManagerSO poolManager;
    [SerializeField] private PoolTypeSO soundPlayerType;

    public void PlayFeedback(Transform trm)
    {
        //SoundPlayer soundPlayer =  poolManager.Pop() as SoundPlayer;
        //soundPlayer.PlaySound(_soundData);
    }
}
