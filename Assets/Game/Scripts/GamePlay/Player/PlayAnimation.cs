using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 播放动画、
    /// 移动时拉伸
    /// </summary>
    public class PlayAnimation : MonoBehaviour
    {
        private Rigidbody2D mRigidbody2D;
        private PlayerMovement mPlayerMovement;
        void Start()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
            mPlayerMovement = GetComponent<PlayerMovement>();
        }


        void Update()
        {
            var velocity = mRigidbody2D.velocity;

            var scaleXOffset = 0.3f * Mathf.Abs(velocity.x / mPlayerMovement.HorizontalMovementSpeed);
            var scale = new Vector3(1f + scaleXOffset, 0.3f + 0.7f, 1);

            transform.localScale = scale;
        }
    }
}
