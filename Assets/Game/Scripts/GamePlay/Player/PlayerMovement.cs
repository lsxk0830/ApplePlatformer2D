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

        public float GravityMultiplier = 2.0f;
        public float FallMultiplier = 1.0f;
        public UnityEvent OnJump;
        public UnityEvent OnLand;

        public float MinJumpTime = 0.2f; // 最小跳跃时间
        public float MaxJumpTime = 0.5f; // 最大跳跃时间

        private float mHorizontalInput = 0f;
        private bool mJumpPressed = false; // 按下跳跃
        private float mCurrentJumpTime = 0f;

        /// <summary>
        /// 横向移动状态
        /// </summary>
        public enum HorizontalMovementStates
        {
            /// <summary>
            /// 停止状态
            /// </summary>
            Stop,
            /// <summary>
            /// 加速状态
            /// </summary>
            Increase,
            /// <summary>
            /// 最大速度
            /// </summary>
            MaxSpeed,
            /// <summary>
            /// 减速状态
            /// </summary>
            Decrease
        }

        public JumpStates JumpState = JumpStates.NotJump;

        public HorizontalMovementStates HorizontalMovementState = HorizontalMovementStates.Stop;

        public Trigger2D GroundCheck;

        private IBonfireRule mDoubleJumpRule;

        private IInputSystem mInputSystem;

        public int LeaveGroundFrame = 0; // 土狼时间
        public int JumpPressFrameCount = -10000; // 输入缓冲

        private void Start()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();

            GroundCheck.OnTriggerEnter.AddListener(() =>
            {
                CurrentJumpCount = 0;

                // 当落地时判断是否缓存了输入
                if (Time.frameCount - JumpPressFrameCount <= 10)
                {
                    JumpStart(); // 触发起跳
                }
            });

            GroundCheck.OnTriggerEnter.AddListener(() =>
            {
                LeaveGroundFrame = Time.frameCount;// 记录主角离开地面时的帧数
            });

            var bonfireSystem = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>();
            mDoubleJumpRule = bonfireSystem.GetRuleByKey(nameof(DoubleJumpRule));
            mInputSystem = ApplePlatformer2D.Interface.GetSystem<IInputSystem>();
        }

        private void OnDestroy()
        {
            mInputSystem = null;
            mDoubleJumpRule = null;
        }

        public int CurrentJumpCount;
        bool mCanJump => CurrentJumpCount == 0 && Time.frameCount - LeaveGroundFrame <= 10 || // 10帧约0.15秒左右
                         CurrentJumpCount == 0 && GroundCheck.Triggered ||
                         mDoubleJumpRule.Unlocked && CurrentJumpCount > 0 && CurrentJumpCount < 2;

        private void JumpStart()
        {
            OnJump?.Invoke();
            mJumpPressed = true;
            CurrentJumpCount++;

            if (JumpState == JumpStates.NotJump)
            {
                JumpState = JumpStates.MinJump;
                mCurrentJumpTime = 0f;
            }
        }

        void Update()
        {
            if (ApplePlatformer2D.IsGameOver) return;

            mHorizontalInput = mInputSystem.HorizontalInput;

            if (mHorizontalInput * transform.localScale.x < 0)
            {
                var localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            }

            if (mInputSystem.JumpDown)
            {
                JumpPressFrameCount = Time.frameCount; // 记录跳跃键按下时的帧数
            }

            if (mInputSystem.JumpDown && mCanJump)
            {
                JumpStart();
            }

            if (mInputSystem.JumpUp)
            {
                mJumpPressed = false;
            }

            mCurrentJumpTime += Time.deltaTime;

            if (mHorizontalInput > 0)
            {
                MovementDirection = 1;
                HorizontalMovementSpeed = MovementDirection * Mathf.Abs(HorizontalMovementSpeed);
            }
            else if (mHorizontalInput < 0)
            {
                MovementDirection = -1;
                HorizontalMovementSpeed = MovementDirection * Mathf.Abs(HorizontalMovementSpeed);
            }

            // 停止、停止 -> 加速
            if (mHorizontalInput != 0 && (HorizontalMovementState == HorizontalMovementStates.Stop || HorizontalMovementState == HorizontalMovementStates.Decrease))
            {
                HorizontalMovementState = HorizontalMovementStates.Increase;
                CurrentHorizontalSpeed = 0;
            } // 加速 -> 最大速度
            else if (mHorizontalInput != 0 && HorizontalMovementState == HorizontalMovementStates.Increase)
            {
                CurrentHorizontalSpeed = Mathf.Lerp(CurrentHorizontalSpeed, HorizontalMovementSpeed, SpeedIncreaseRate);
                //Debug.Log($"Current:{CurrentHorizontalSpeed},Horizontal:{HorizontalMovementSpeed}");
                if (Mathf.Abs(CurrentHorizontalSpeed - HorizontalMovementSpeed) < 0.1f)
                {
                    CurrentHorizontalSpeed = HorizontalMovementSpeed;
                    HorizontalMovementState = HorizontalMovementStates.MaxSpeed;
                }
            } // 最大速度
            else if (mHorizontalInput != 0 && HorizontalMovementState == HorizontalMovementStates.MaxSpeed)
            {
                CurrentHorizontalSpeed = HorizontalMovementSpeed;
            } // 最大速度-> 减速
            else if (mHorizontalInput == 0 && HorizontalMovementState == HorizontalMovementStates.MaxSpeed)
            {
                HorizontalMovementState = HorizontalMovementStates.Decrease;
            } // 速度-> 停止
            else if (mHorizontalInput == 0 && HorizontalMovementState == HorizontalMovementStates.Decrease)
            {
                CurrentHorizontalSpeed = Mathf.Lerp(CurrentHorizontalSpeed, 0, SpeedDecreaseRate);
                if (Mathf.Abs(CurrentHorizontalSpeed) < 0.1f)
                {
                    CurrentHorizontalSpeed = 0;
                    HorizontalMovementState = HorizontalMovementStates.Stop;
                }
            }
        }

        public float CurrentHorizontalSpeed = 0;
        public float MovementDirection = 1; // 移动方向
        public enum JumpStates
        {
            NotJump,
            MinJump,
            ControlJump,
        }

        public float SpeedIncreaseRate = 0.8f; // 速度递增速率
        public float SpeedDecreaseRate = 0.8f; // 速度递减速率
        private void FixedUpdate()
        {
            if (JumpState == JumpStates.MinJump)
            {
                mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, JumpSpeed); // 设置速度--垂直

                if (mCurrentJumpTime >= MinJumpTime)
                {
                    JumpState = JumpStates.ControlJump;
                }
            }
            else if (JumpState == JumpStates.ControlJump)
            {
                mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, JumpSpeed); // 设置速度--垂直

                if (!mJumpPressed || (mJumpPressed && mCurrentJumpTime >= MaxJumpTime))
                {
                    JumpState = JumpStates.NotJump;
                }
            }

            // 横向移动的核心代码
            mRigidbody2D.velocity = new Vector2(CurrentHorizontalSpeed, mRigidbody2D.velocity.y); // 设置速度--水平

            // 调整跳跃上升、下落重力
            if (mRigidbody2D.velocity.y > 0 && JumpState != JumpStates.NotJump)
            {
                var progress = mCurrentJumpTime / MaxJumpTime;
                float jumpGravityMultiplier = GravityMultiplier;
                if (progress > 0.5f)
                {
                    jumpGravityMultiplier = GravityMultiplier * (1 - progress);
                }
                mRigidbody2D.velocity += Physics2D.gravity * jumpGravityMultiplier * Time.deltaTime;
            }
            else if (mRigidbody2D.velocity.y < 0)
            {
                mRigidbody2D.velocity += Physics2D.gravity * FallMultiplier * Time.deltaTime;
            }
        }
    }
}