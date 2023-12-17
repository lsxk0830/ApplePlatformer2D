using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 玩家冲刺
    /// </summary>
    public class PlayerDash : MonoBehaviour
    {
        private Rigidbody2D mRigidbody2D;
        private DashRule mDashRule;
        public float Speed = 20;

        /// <summary>
        /// 冲刺时间
        /// </summary>
        public float Duration = 0.5f;

        /// <summary>
        /// 冲刺之前的重力数值
        /// </summary>
        private float mCachedGravityScale = 0;

        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
            mDashRule = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(nameof(DashRule)) as DashRule;
        }

        private float mDashStartTime = 0f;
        private bool Dashing = false;
        private void Update()
        {
            if (mDashRule.Unlocked)
            {
                if (Input.GetKeyDown(KeyCode.O))
                {
                    mRigidbody2D.velocity = Vector2.right * Speed * Mathf.Sign(transform.localScale.x);
                    GetComponent<PlayerMovement>().enabled = false;

                    mCachedGravityScale = mRigidbody2D.gravityScale;
                    mRigidbody2D.gravityScale = 0;

                    mDashStartTime = Time.time;
                    Dashing = true;
                }

                if (Dashing && mDashStartTime + Duration < Time.time)
                {
                    Dashing = false;
                    mRigidbody2D.gravityScale = mCachedGravityScale;
                    GetComponent<PlayerMovement>().enabled = true;
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