using System;
using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    public class SimpleFollowEnemy : MonoBehaviour
    {
        public float FollowPlayerSpeed = 6;
        public float WarningSeconds = 0.3f;
        public enum States
        {
            Idle,
            Warning,
            Following
        }

        private FSM<States> mFSM = new FSM<States>();

        public Trigger2D GroundCheck;
        public Trigger2D ForwardCheck;
        public Trigger2D FallCheck;
        public Trigger2D AttackCheck;
        public Trigger2D WarningCheck;

        public UnityEvent OnIdle = new UnityEvent();
        public UnityEvent OnWarning = new UnityEvent();
        public UnityEvent OnFollowing = new UnityEvent();

        private void Awake()
        {
            var characterMovement = GetComponent<CharacterMovement>();
            characterMovement.enabled = false;

            mFSM.State(States.Idle)
            .OnEnter(() =>
            {
                OnIdle?.Invoke();
                characterMovement.enabled = true;
                characterMovement.HorizontalMovement = 3;
            });

            float currentWarningSeconds = 0;
            mFSM.State(States.Warning)
            .OnEnter(() =>
            {
                OnWarning?.Invoke();
                characterMovement.enabled = false;
                characterMovement.HorizontalMovement = 3;
            })
            .OnUpdate(() =>
            {
                if (currentWarningSeconds > WarningSeconds)
                {
                    mFSM.ChangeState(States.Following);
                }
                currentWarningSeconds += Time.deltaTime;
            });

            var playerGameObject = GameObject.FindWithTag("Player");
            mFSM.State(States.Following)
            .OnEnter(() =>
            {
                OnFollowing?.Invoke();
                characterMovement.enabled = true;
                characterMovement.HorizontalMovement = FollowPlayerSpeed;
            })
            .OnFixedUpdate(() =>
            {
                var playerPos = playerGameObject.transform.position;
                var enemyPos = transform.position;
                var moveDirection = playerPos - enemyPos;
                if (moveDirection.x > 0)
                {
                    var scale = transform.localScale;
                    scale.x = 1;
                    transform.localScale = scale;
                }
                else
                {
                    var scale = transform.localScale;
                    scale.x = -1;
                    transform.localScale = scale;
                }
            });

            mFSM.StartState(States.Idle);

            WarningCheck.OnTriggerEnter.AddListener(() =>
            {
                mFSM.ChangeState(States.Warning);
            });
            WarningCheck.OnTriggerExit.AddListener(() =>
            {
                mFSM.ChangeState(States.Idle);
            });

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

            AttackCheck.OnTriggerEnterWithCollider.AddListener((collider) =>
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

        private void Update()
        {
            mFSM.Update();
        }

        private void OnDestroy()
        {
            mFSM.Clear();
            mFSM = null;
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