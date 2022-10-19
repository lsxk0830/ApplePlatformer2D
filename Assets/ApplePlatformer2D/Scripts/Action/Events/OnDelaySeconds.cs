using System.Collections;
using UnityEngine;
using UnityEngine.Events;

namespace UTGM
{
    public class OnDelaySeconds : MonoBehaviour
    {
        public float Seconds = 1;
        public UnityEvent OnDelayFinish = new UnityEvent();

        IEnumerator Start()
        {
            yield return new WaitForSeconds(Seconds);

            OnDelayFinish?.Invoke();    
        }
    }

}
