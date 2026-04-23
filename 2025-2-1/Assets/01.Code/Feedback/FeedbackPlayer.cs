using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Code.Feedback
{
    public class FeedbackPlayer : MonoBehaviour
    {
        [SerializeField] private bool useTargetTrm;
        [SerializeField] private Transform targetTrm;

        private List<IFeedback> _feedbacks;
        
        private void Awake()
        {
            _feedbacks = GetComponents<IFeedback>().ToList();
        }
        
        public void PlayFeedback(Transform trm)
        {
            if (useTargetTrm) trm = targetTrm;
            
            foreach (var feedback in _feedbacks)
            {
                feedback.PlayFeedback(trm);
            }
        }
        
        public void PlayFeedback()
        {
            PlayFeedback(transform);
        }
    }
}