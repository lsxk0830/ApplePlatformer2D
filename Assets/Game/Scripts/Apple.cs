using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    public class Apple : MonoBehaviour
    {
        public UnityEvent OnGet;

        private void OnTriggerEnter2D(Collider2D other)
        {
            OnGet?.Invoke();

            Destroy(gameObject);
        }
    }
}