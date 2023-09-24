using UnityEngine.Events;
using UnityEngine;
using System.Linq;

namespace Blue
{
    public class OnTriggerEnter2DEvent : MonoBehaviour
    {
        public string[] Tags;
        public UnityEvent OnEnter = new UnityEvent();

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (Tags.Any(tag => other.CompareTag(tag)))
            {
                OnEnter?.Invoke();
            }
        }
    }
}