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

        private float mPreviousHitTime = 0;
        public bool CanHit => Time.time - mPreviousHitTime > 1.0f;

        public void Hit()
        {
            if (CanHit)
            {
                OnHit?.Invoke();
                mPreviousHitTime = Time.time;
            }
        }
    }
}