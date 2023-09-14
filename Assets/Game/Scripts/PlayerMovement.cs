using UnityEngine;
using UnityEngine.Events;

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

        public UnityEvent OnJump;
        public UnityEvent OnLand;

        public float MinJumpTime = 0.2f; // 最小跳跃时间
        public float MaxJumpTime = 0.5f; // 最大跳跃时间

        private float mHorizontalInput = 0f;
        private bool mJumpPressed = false; // 按下跳跃
        private float mCurrentJumpTime = 0f;

        public JumpStates JumpState = JumpStates.NotJump;
        private void Start()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
        }


        void Update()
        {
            mHorizontalInput = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.K) && CollisionObjectCount > 0)
            {
                OnJump?.Invoke();
                mJumpPressed = true;

                if(JumpState == JumpStates.NotJump)
                {
                    JumpState = JumpStates.MinJump;
                    mCurrentJumpTime = 0f;
                }
            }
            if (Input.GetKeyUp(KeyCode.K))
            {
                mJumpPressed = false;
            }

            mCurrentJumpTime += Time.deltaTime;
        }

        public enum JumpStates
        {
            NotJump,
            MinJump,
            ControlJump,
        }

        private void FixedUpdate()
        {
            if(JumpState ==JumpStates.MinJump)
            {
                mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, JumpSpeed); // 设置速度--垂直

                if(mCurrentJumpTime>=MinJumpTime)
                {
                    JumpState = JumpStates.ControlJump;
                }
            }
            else if(JumpState == JumpStates.ControlJump)
            {
                mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, JumpSpeed); // 设置速度--垂直

                if(!mJumpPressed || (mJumpPressed && mCurrentJumpTime>=MaxJumpTime))
                {
                    JumpState = JumpStates.NotJump;
                }
            }

            mRigidbody2D.velocity = new Vector2(mHorizontalInput * HorizontalMovementSpeed, mRigidbody2D.velocity.y); // 设置速度--水平

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
            OnLand?.Invoke();
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            CollisionObjectCount--;
        }
    }
}