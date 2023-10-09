using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    /// <summary>
    /// 角色受伤
    /// </summary>
    public class CharacterHit : MonoBehaviour
    {
        public UnityEvent OnHit = new UnityEvent();

        public void Hit()
        {
            OnHit?.Invoke();
        }
    }
}