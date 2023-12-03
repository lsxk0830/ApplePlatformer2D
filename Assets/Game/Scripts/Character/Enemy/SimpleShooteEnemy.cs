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
        public Trigger2D ShootArea;
        public OnTriggerStay2DEvent AttackCheck;

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

            ShootArea.OnTriggerEnter.AddListener(() =>
            {
                state = States.Shoot;
                mPreviousShootTime = Time.time - 0.8f;
                characterMovement.enabled = false;
            });
            ShootArea.OnTriggerExit.AddListener(() =>
            {
                state = States.Patrol;
                characterMovement.enabled = true;
            });
            AttackCheck.OnStayWithCollider.AddListener(collider=>
            {
                var playerHit = collider.GetComponent<PlayerHit>();
                if(playerHit.CanHit)
                {
                    playerHit.Hit();
                }
            });
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