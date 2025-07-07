using System;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Code.Sound
{
    public class BGMPlayer : MonoBehaviour
    {
        public UnityEvent OnPlay;

        private void Start()
        {
            OnPlay?.Invoke();
        }
    }
}