using System;
using UnityEngine;

namespace Blue
{
    public class SimpleShooteEnemy : CharacterController
    {
        public enum States
        {
            /// <summary>
            /// 落下
            /// </summary>
            Falling,
            /// <summary>
            /// 巡逻
            /// </summary>
            Patrol,
            /// <summary>
            /// 攻击
            /// </summary>
            Shoot
        }

        public States state = States.Falling;
        public Trigger2D GroundCheck;
        public Trigger2D ForwardCheck;
        public Trigger2D FallCheck;
        public Trigger2D AttackCheck;
        public Trigger2D ShootArea;

        public GameObject BulletPrefab;

        private void Awake()
        {
            var characterMovement = GetComponent<CharacterMovement>();
            characterMovement.enabled = false;
            GroundCheck.OnTriggerEnter.AddListener(() =>
            {
                state = States.Patrol; // 巡逻状态
                // 触发这个之后，在进行移动
                characterMovement.enabled = true;
            });

            GroundCheck.OnTriggerExit.AddListener(() =>
            {
                state = States.Falling; // 下落状态
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

            AttackCheck.OnTriggerEnterWithCollider.AddListener((collider) =>
            {
                collider.GetComponent<PlayerHit>().Hit();

                // 玩家攻击主角后，主角向后跳跃一下
                AttackPhysicsEffect(transform, collider.transform);
            });

            ShootArea.OnTriggerEnter.AddListener(() =>
            {
                state = States.Shoot;
                mPreviousShootTime = Time.time;
                characterMovement.enabled = false;
            });
            ShootArea.OnTriggerExit.AddListener(() =>
            {
                state = States.Patrol;
                characterMovement.enabled = true;
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

        private float mPreviousShootTime; // 上一次攻击时间
        private void Update()
        {
            if (state == States.Shoot)
            {
                // 射击操作
                if (Time.time - mPreviousShootTime > 1.0f)
                {
                    var bulletObject = Instantiate(BulletPrefab);
                    bulletObject.transform.position = BulletPrefab.transform.position;
                    bulletObject.SetActive(true);
                    bulletObject.GetComponent<Rigidbody2D>().velocity = Vector2.right * transform.localScale.x * 6;

                    mPreviousShootTime = Time.time;
                }
            }
        }
    }
}