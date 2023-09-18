using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    public class PlayerHit : MonoBehaviour
    {
        public UnityEvent OnHit = new UnityEvent();

        public void Hit()
        {
            OnHit?.Invoke();
        }
    }
}