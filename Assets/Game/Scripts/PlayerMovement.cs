using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 玩家移动控制
    /// </summary>
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D mRigidbody2D;
        public float HorizontalMovementSpeed = 5; // 水平移动速度
        public float JumpSpeed = 5; // 跳跃速度

        public float JumpUpGravity = 3; // 跳跃时上升时重力
        public float FallDownGravity = 6; // 跳跃时下落时重力
        private void Start()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.K) && CollisionObjectCount > 0)
            {
                mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, JumpSpeed); // 设置速度--垂直
            }

            mRigidbody2D.velocity = new Vector2(horizontal * HorizontalMovementSpeed, mRigidbody2D.velocity.y); // 设置速度--水平

            // 调整跳跃上升、下落重力
            if (mRigidbody2D.velocity.y > 0)
                mRigidbody2D.gravityScale = JumpUpGravity;
            else
                mRigidbody2D.gravityScale = FallDownGravity;
        }

        public int CollisionObjectCount;
        private void OnCollisionEnter2D(Collision2D other)
        {
            CollisionObjectCount++;
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            CollisionObjectCount--;
        }
    }
}