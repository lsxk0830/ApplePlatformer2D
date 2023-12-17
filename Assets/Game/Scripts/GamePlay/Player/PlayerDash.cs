using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    /// <summary>
    /// 玩家冲刺
    /// </summary>
    public class PlayerDash : MonoBehaviour
    {
        private Rigidbody2D mRigidbody2D;
        private DashRule mDashRule;

        public UnityEvent OnDash = new UnityEvent();

        public float Speed = 20;

        /// <summary>
        /// 冲刺时间
        /// </summary>
        public float Duration = 0.5f;

        /// <summary>
        /// 是否可冲刺
        /// </summary>
        private bool mCanDash = true;

        /// <summary>
        /// 冲刺之前的重力数值
        /// </summary>
        private float mCachedGravityScale = 0;

        public Trigger2D GroundCheck;

        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
            mDashRule = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(nameof(DashRule)) as DashRule;

            GroundCheck.OnTriggerEnter.AddListener(()=>
            {
                mCanDash = true; // 落地重置冲刺状态
            });
        }

        private float mDashStartTime = 0f;

        /// <summary>
        /// 冲刺中
        /// </summary>
        private bool Dashing = false;

        private void Update()
        {
            if (mDashRule.Unlocked)
            {
                if (Input.GetKeyDown(KeyCode.O) && !Dashing && mCanDash)
                {
                    mRigidbody2D.velocity = Vector2.right * Speed * Mathf.Sign(transform.localScale.x);
                    GetComponent<PlayerMovement>().enabled = false;

                    mCachedGravityScale = mRigidbody2D.gravityScale;
                    mRigidbody2D.gravityScale = 0;

                    mDashStartTime = Time.time;
                    Dashing = true;
                    mCanDash = false;

                    OnDash?.Invoke();
                }

                if (Dashing && mDashStartTime + Duration < Time.time)
                {
                    Dashing = false;
                    mRigidbody2D.gravityScale = mCachedGravityScale;
                    GetComponent<PlayerMovement>().enabled = true;

                    // 冲刺结束时，如果是落地状态，则重置
                    if(GroundCheck.Triggered)
                    {
                        mCanDash = true;
                    }
                }
            }
        }

        private void OnDestroy()
        {
            mRigidbody2D = null;
            mDashRule = null;
        }
    }
}