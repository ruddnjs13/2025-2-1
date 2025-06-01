using System;
using _01.Code.Tower;
using UnityEngine;
using UnityEngine.Events;

namespace _01.Code.ETC
{
    public class AnimationTrigger : MonoBehaviour
    {
        public Action OnAttack;
        
        public void Attack() => OnAttack?.Invoke();
    }
}