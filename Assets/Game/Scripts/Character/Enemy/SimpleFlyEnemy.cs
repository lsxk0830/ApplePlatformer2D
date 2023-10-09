using UnityEngine;

namespace Blue
{
    public class SimpleFlyEnemy : MonoBehaviour
    {
        public Trigger2D WarnigArea;
        public Trigger2D AttackArea;

        public enum States
        {
            Idle,
            FollowPlayer,
        }

        private FSM<States> mFSM = new FSM<States>();
        private Rigidbody2D mRigidbody2D;
        public float MovementSpeed = 5;

        private void Awake()
        {
            mRigidbody2D = GetComponent<Rigidbody2D>();
            var playerTransform = GameObject.FindWithTag("Player").transform;

            mFSM.State(States.Idle).OnEnter(() =>
            {
                mRigidbody2D.velocity = Vector2.zero;
            });
            mFSM.State(States.FollowPlayer).OnFixedUpdate(() =>
            {
                var playerPos = playerTransform.position;
                var enemyPos = transform.position;
                var direction = playerPos - enemyPos;
                direction = direction.normalized;
                mRigidbody2D.velocity = direction * MovementSpeed;
            });

            mFSM.StartState(States.Idle);

            WarnigArea.OnTriggerEnter.AddListener(()=>
            {
                mFSM.ChangeState(States.FollowPlayer);
            });
            WarnigArea.OnTriggerExit.AddListener(() =>
            {
                mFSM.ChangeState(States.Idle);
            });

            AttackArea.OnTriggerEnter.AddListener(()=>
            {
                playerTransform.GetComponent<PlayerHit>().Hit();
            });
        }

        private void FixedUpdate()
        {
            mFSM.FixedUpdate();
        }

        private void OnDestroy()
        {
            mFSM.Clear();
            mFSM = null;
        }
    }
}