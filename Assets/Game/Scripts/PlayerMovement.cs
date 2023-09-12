using UnityEngine;

namespace Blue
{
    public class PlayerMovement : MonoBehaviour
    {
        private Rigidbody2D mRigidbody2D;
        private void Start()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            var horizontal = Input.GetAxis("Horizontal");

            if (Input.GetKeyDown(KeyCode.K))
            {
                mRigidbody2D.velocity = new Vector2(mRigidbody2D.velocity.x, 5); // 设置速度--垂直
            }

            mRigidbody2D.velocity = new Vector2(horizontal, mRigidbody2D.velocity.y); // 设置速度--水平
        }
    }
}