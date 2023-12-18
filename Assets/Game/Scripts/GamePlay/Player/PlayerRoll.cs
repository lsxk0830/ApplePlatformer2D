using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 主角翻滚
    /// </summary>
    public class PlayerRoll : MonoBehaviour
    {
        private Rigidbody2D mRigdbody2D;

        private SpriteRenderer mSpriteRenderer;

        /// <summary>
        /// 翻滚时长
        /// </summary>
        public float Duration = 0.5f;
        public float Speed = 3f;
        private float mRollStartTime = 0;
        public bool Rolling = false;

        private RollRule mRollRule;

        private void Awake()
        {
            mRigdbody2D = GetComponent<Rigidbody2D>();
            mSpriteRenderer = GetComponent<SpriteRenderer>();
            mRollRule = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(nameof(RollRule)) as RollRule;
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L) && !Rolling && mRollRule.Unlocked)
            {
                Rolling = true;

                mRollStartTime = Time.time;
                GetComponent<PlayerMovement>().enabled = false;
                GetComponent<PlayerDash>().enabled = false;
                GetComponent<PlayerHit>().SetCanHit(false);
                GetComponent<PlayerFootAttack>().enabled = false;
                gameObject.tag = "Untagged";
                gameObject.layer = LayerMask.NameToLayer("WithoutEnemy");
            }
            else if (Rolling)
            {
                mRigdbody2D.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * Speed, mRigdbody2D.velocity.y);

                // 从 0 ~0.1f秒，开始逐渐透明
                var fromStart = Time.time - mRollStartTime;
                if (fromStart < 0.1f)
                {
                    var alpha = Mathf.Lerp(1f, 0.5f, fromStart * 10f);
                    mSpriteRenderer.color = new Color(1, 1, 1, alpha);
                }
                // 从 Duration - 0.1秒 ~ Duration 秒，开始逐渐恢复
                var toEnd = mRollStartTime + Duration - Time.time;
                if (toEnd > 0 && toEnd < 0.1f)
                {
                    var alpha = Mathf.Lerp(0.5f, 1f, (0.1f - toEnd) * 10f);
                    mSpriteRenderer.color = new Color(1, 1, 1, alpha);
                }

                if (Time.time > mRollStartTime + Duration)
                {
                    Rolling = false;
                    GetComponent<PlayerMovement>().enabled = true;
                    GetComponent<PlayerDash>().enabled = true;
                    GetComponent<PlayerHit>().SetCanHit(true);
                    GetComponent<PlayerFootAttack>().enabled = true;
                    gameObject.tag = "Player";
                    gameObject.layer = LayerMask.NameToLayer("Player");
                    mSpriteRenderer.color = new Color(1, 1, 1, 1);
                }
            }
        }

        private void OnDestroy()
        {
            mRigdbody2D = null;
            mRollRule = null;
            mSpriteRenderer = null;
        }
    }
}