using System;
using UnityEngine;

namespace Blue
{
    public class SimpleEnemy : CharacterController
    {
        public Trigger2D GroundCheck;
        public Trigger2D ForwardCheck;
        public Trigger2D FallCheck;
        public OnTriggerStay2DEvent AttackCheck;

        private void Awake()
        {
            var characterMovement = GetComponent<CharacterMovement>();
            characterMovement.enabled = false;
            GroundCheck.OnTriggerEnter.AddListener(() =>
            {
                // 触发这个之后，在进行移动
                characterMovement.enabled = true;
            });

            GroundCheck.OnTriggerExit.AddListener(() =>
            {
                characterMovement.enabled = false;
            });

            ForwardCheck.OnTriggerEnter.AddListener(() =>
            {
                var localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            });

            FallCheck.OnTriggerExit.AddListener(() =>
            {
                var localScale = transform.localScale;
                localScale.x *= -1;
                transform.localScale = localScale;
            });

            AttackCheck.OnStayWithCollider.AddListener((collider) =>
            {
                var playerHit = collider.GetComponent<PlayerHit>();
                if (playerHit.CanHit)
                {
                    playerHit.Hit();
                    // 玩家攻击主角后，主角向后跳跃一下
                    AttackPhysicsEffect(transform, collider.transform);
                }
            });
        }

        public int HitterVelocityX = 5; // 攻击时水平方向速度
        public int HitterVelocityY = 5; // 攻击时垂直方向速度

        // 物理攻击效果,想斜后方跳跃
        void AttackPhysicsEffect(Transform attack, Transform hitter)
        {
            var attackPos = attack.position.x;
            var hitPos = hitter.position.x;
            var direction = hitPos - attackPos;
            var directionNormal = MathF.Sign(direction);
            hitter.GetComponent<Rigidbody2D>().velocity = new Vector2(directionNormal * HitterVelocityX, HitterVelocityY);
        }
    }
}