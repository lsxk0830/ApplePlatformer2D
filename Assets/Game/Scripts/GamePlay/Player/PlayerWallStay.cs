using UnityEngine;

namespace Blue
{
    public class PlayerWallStay : MonoBehaviour
    {
        public Trigger2D WallCheck;
        public Trigger2D GroundCheck;
        private Rigidbody2D mRigidbody2D;

        /// <summary>
        /// 在墙上
        /// </summary>
        public bool OnWall = false;
        /// <summary>
        /// 在墙上跳跃中
        /// </summary>
        public bool WallJumping = false;
        /// <summary>
        /// 在墙上时的跳跃方向
        /// </summary>
        private float mWallJumpDirection = 1;
        /// <summary>
        /// 缓存的重力
        /// </summary>
        private float mCachedGravityScale;
        private float mWallJumpStartTime = 0;

        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();

            WallCheck.OnTriggerEnter.AddListener(() =>
            {
                if (!OnWall)
                {
                    var horizontalInput = ApplePlatformer2D.Interface.GetSystem<IInputSystem>().HorizontalInput;

                    if (horizontalInput * transform.localScale.x > 0) // 方向相同
                    {
                        if (!GroundCheck.Triggered)
                        {
                            // 触发挂墙
                            OnWall = true;
                            GetComponent<PlayerMovement>().enabled = false;
                            var rigidbody2D = GetComponent<Rigidbody2D>();
                            mCachedGravityScale = rigidbody2D.gravityScale;
                            rigidbody2D.gravityScale = 0;
                            rigidbody2D.velocity = Vector2.zero;

                            // 重置跳跃次数
                            GetComponent<PlayerMovement>().CurrentJumpCount = 0;
                            GetComponent<PlayerMovement>().OnWall = true;
                        }
                    }
                }
            });
        }

        private void Update()
        {
            if (OnWall)
            {
                var horizontalInput = ApplePlatformer2D.Interface.GetSystem<IInputSystem>().HorizontalInput;

                if (horizontalInput * transform.localScale.x < 0) // 方向相同
                {
                    if (!GroundCheck.Triggered)
                    {
                        // 取消挂墙
                        OnWall = false;
                        GetComponent<PlayerMovement>().enabled = true;
                        var rigidbody2D = GetComponent<Rigidbody2D>();
                        rigidbody2D.gravityScale = mCachedGravityScale;
                    }
                }

                if (ApplePlatformer2D.Interface.GetSystem<IInputSystem>().JumpDown)
                {
                    OnWall = false;
                    WallJumping = true;
                    mWallJumpStartTime = Time.time;

                    GetComponent<PlayerMovement>().enabled = true;
                    var rigidbody2D = GetComponent<Rigidbody2D>();
                    rigidbody2D.gravityScale = mCachedGravityScale;

                    // 转向
                    var localScale = transform.localScale;
                    localScale.x *= -1;
                    transform.localScale = localScale;

                    mWallJumpDirection = Mathf.Sign(localScale.x);
                    GetComponent<PlayerMovement>().JumpState = PlayerMovement.JumpStates.NotJump;
                    GetComponent<PlayerMovement>().JumpStart();
                }
            }

            if (WallJumping)
            {
                GetComponent<PlayerMovement>().controlleredHorizontalInput = mWallJumpDirection * 2;

                if (Time.time - mWallJumpStartTime > 0.2f) // 持续 0.3f
                {
                    WallJumping = false;
                    GetComponent<PlayerMovement>().controlleredHorizontalInput = 0;
                }
            }
        }
    }
}