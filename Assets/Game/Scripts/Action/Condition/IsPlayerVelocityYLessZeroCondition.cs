using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    /// <summary>
    /// 玩家Y的速度小于0时
    /// </summary>
    public class IsPlayerVelocityYLessZeroCondition : MonoBehaviour
    {
        public UnityEvent IsLess = new UnityEvent();

        public void Execute()
        {
            if (GameObject.FindWithTag("Player").GetComponent<Rigidbody2D>().velocity.y <= 0)
            {
                IsLess?.Invoke();
            }
        }
    }
}