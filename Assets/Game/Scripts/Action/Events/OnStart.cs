using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    public class OnStart : MonoBehaviour
    {
        public UnityEvent OnStartEvent = new UnityEvent();

        private void Start()
        {
            OnStartEvent?.Invoke();
        }
    }
}