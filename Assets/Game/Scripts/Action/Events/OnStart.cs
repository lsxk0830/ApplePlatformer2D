using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    public class OnStart : MonoBehaviour
    {
        public UnityEvent OnStartEvent = new UnityEvent();

        public bool CallOnceInGlobsl = false;
        private static bool mCalled = false;

        private void Start()
        {
            if (CallOnceInGlobsl)
            {
                if (!mCalled)
                {
                    OnStartEvent?.Invoke();
                    mCalled = true;
                }
            }
            else
            {
                OnStartEvent?.Invoke();
            }
        }
    }
}