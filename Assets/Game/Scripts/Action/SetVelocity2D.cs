using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 给指定物体设置速度
    /// </summary>
    public class SetVelocity2D : MonoBehaviour
    {
        public Rigidbody2D Rigidbody2D;
        public Vector2 Vector2;

        public void Execute()
        {
            Rigidbody2D.velocity = Vector2;
        }
    }
}