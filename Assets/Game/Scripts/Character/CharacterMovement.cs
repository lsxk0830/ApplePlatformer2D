using UnityEngine;

namespace Blue
{
    public class CharacterMovement : MonoBehaviour
    {
        private Rigidbody2D mRigidbody2D;

        public float HorizontalMovement = 3; // 速度
        void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
        }

        void Update()
        {
            mRigidbody2D.velocity = new Vector2(transform.localScale.x * HorizontalMovement,mRigidbody2D.velocity.y);
        }
    }
}