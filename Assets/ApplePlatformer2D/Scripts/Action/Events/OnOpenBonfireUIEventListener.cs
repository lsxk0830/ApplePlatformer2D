using QFramework;
using UnityEngine;
using UnityEngine.Events;

namespace UTGM
{
    public class OnOpenBonfireUIEventListener : MonoBehaviour
    {
        public UnityEvent OnOpenBonfireUI = new UnityEvent();
        void Start()
        {
            ApplePlatformer2D.OnOpenBonfireUI.Register(() =>
            {
                OnOpenBonfireUI?.Invoke();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}