using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    /// <summary>
    /// 玩家受伤
    /// </summary>
    public class PlayerHit : MonoBehaviour
    {
        public UnityEvent OnHit = new UnityEvent();

        public void Hit()
        {
            OnHit?.Invoke();
        }
    }
}