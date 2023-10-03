using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    public class OnDelaySeconds : MonoBehaviour
    {
        public float Seconds = 1f;

        public UnityEvent OnDelayFinish = new UnityEvent();

        IEnumerator Start()
        {
            yield return new WaitForSeconds(Seconds);

            OnDelayFinish?.Invoke();
        }
    }
}