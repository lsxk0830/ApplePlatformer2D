using QFramework;
using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    /// <summary>
    /// 打开火堆UI事件监听，ApplePlatformer2D.OnOpenBonfireUI事件触发时触发OnOpenBonfireUI事件
    /// </summary>
    public class OnOpenBonfireUIEventListener : MonoBehaviour
    {
        public UnityEvent OnOpenBonfireUI = new UnityEvent();

        private void Start()
        {
            ApplePlatformer2D.OnOpenBonfireUI.Register(() =>
            {
                OnOpenBonfireUI?.Invoke();
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}