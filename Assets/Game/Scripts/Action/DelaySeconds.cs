using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    /// <summary>
    /// 延迟时间
    /// </summary>
    public class DelaySeconds : MonoBehaviour
    {
        public float Seconds;
        public UnityEvent OnFinish = new UnityEvent();

        public void Execute()
        {
            StartCoroutine(DoDelay());
        }

        private IEnumerator DoDelay()
        {
            yield return new WaitForSeconds(Seconds);
            OnFinish?.Invoke();
        }
    }
}