using System;
using UnityEngine;
using UnityEngine.Events;

namespace Code.ETC
{
    public class AnimationTrigger : MonoBehaviour
    {
        public Action OnAttack;
        
        public void Attack() => OnAttack?.Invoke();
    }
}