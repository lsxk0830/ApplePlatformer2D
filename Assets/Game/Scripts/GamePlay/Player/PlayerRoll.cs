using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 主角翻滚
    /// </summary>
    public class PlayerRoll : MonoBehaviour
    {
        private Rigidbody2D mRigdbody2D;

        /// <summary>
        /// 翻滚时长
        /// </summary>
        public float Duration = 0.5f;
        public float Speed = 3f;
        private float mRollStartTime = 0;
        public bool Rolling = false;

        private void Awake()
        {
            mRigdbody2D = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L) && !Rolling)
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
                mRigdbody2D.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * 5, mRigdbody2D.velocity.y);
                if (Time.time > mRollStartTime + Duration)
                {
                    Rolling = false;
                    GetComponent<PlayerMovement>().enabled = true;
                    GetComponent<PlayerDash>().enabled = true;
                    GetComponent<PlayerHit>().SetCanHit(true);
                    GetComponent<PlayerFootAttack>().enabled = true;
                    gameObject.tag = "Player";
                    gameObject.layer = LayerMask.NameToLayer("Player");
                }
            }
        }

        private void OnDestroy()
        {
            mRigdbody2D = null;
        }
    }
}