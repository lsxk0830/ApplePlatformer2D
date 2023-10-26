using System.Collections;
using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 时间缩放
    /// </summary>
    public class ScaleTimeEffect : MonoBehaviour
    {
        public float TimeScale = 0.2f;
        public float Duration = 0.2f; // 持续时间

        public void Execute()
        {
            StartCoroutine(DoExecute());
        }

        private IEnumerator DoExecute()
        {
            Time.timeScale = TimeScale;
            yield return new WaitForSeconds(Duration);
            Time.timeScale = 1f;
        }
    }
}