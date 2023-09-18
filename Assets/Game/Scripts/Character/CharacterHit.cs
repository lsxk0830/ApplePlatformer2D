using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    public class CharacterHit : MonoBehaviour
    {
        public UnityEvent OnHit = new UnityEvent();

        public void Hit()
        {
            OnHit?.Invoke();
        }
    }
}