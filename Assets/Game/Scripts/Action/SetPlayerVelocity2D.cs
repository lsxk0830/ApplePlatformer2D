using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 设置玩家速度
    /// </summary>
    public class SetPlayerVelocity2D : MonoBehaviour
    {
        public Vector2 Velocity;

        public void Execute()
        {
            GameObject.FindWithTag("Player")
                .GetComponent<Rigidbody2D>().velocity = Velocity;
        }
    }
}