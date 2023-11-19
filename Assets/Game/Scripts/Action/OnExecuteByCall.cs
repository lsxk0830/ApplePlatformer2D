using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    /// <summary>
    /// 执行回调
    /// </summary>
    public class OnExecuteByCall : MonoBehaviour
    {
        public UnityEvent OnExecute = new UnityEvent();

        public void Execute()
        {
            OnExecute?.Invoke();
        }
    }
}