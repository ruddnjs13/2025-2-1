using UnityEngine;
using UnityEngine.Events;

namespace Code.UI
{
    public class SceneFade : MonoBehaviour
    {
        public UnityEvent SceneFadeEvent;
        private void Start()
        {
            SceneFadeEvent?.Invoke();
        }
    }
}
