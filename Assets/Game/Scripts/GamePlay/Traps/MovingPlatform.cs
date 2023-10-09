using UnityEngine;

namespace Blue
{
    public class MovingPlatform : MonoBehaviour
    {
        public enum States
        {
            Stop,
            Moving
        }
        public FSM<States> FSM = new FSM<States>();
        public Transform Pos1;
        public Transform Pos2;

        public float MovementSpeed = 5; // 移速
        public float StopSeconds = 2; // 停顿时间
        private Vector3 mPos1;
        private Vector3 mPos2;
        private Vector3 mToPosition; // 目标

        private void Awake()
        {
            mPos1 = Pos1.position;
            mPos2 = Pos2.position;

            mToPosition = mPos2;

            var stopTime = Time.time;

            FSM.State(States.Stop)
            .OnEnter(() => { stopTime = Time.time; })
            .OnUpdate(() =>
            {
                if (Time.time - stopTime >= StopSeconds)
                {
                    FSM.ChangeState(States.Moving);
                }
            });

            FSM.State(States.Moving)
            .OnEnter(() => { mToPosition = mToPosition == mPos2 ? mPos1 : mPos2;})
            .OnUpdate(() =>
            {
                var currentPosition = transform.position;
                var moveDirection = mToPosition - currentPosition; // 移动方向
                moveDirection = moveDirection.normalized; // 归一化
                transform.Translate(moveDirection * MovementSpeed * Time.deltaTime);
                if (Vector3.Distance(currentPosition, mToPosition) < 0.1f)
                {
                    FSM.ChangeState(States.Stop);
                }
            });

            FSM.StartState(States.Stop);
        }

        private void Update()
        {
            FSM.Update();
        }
        private void OnDestroy()
        {
            FSM.Clear();
            FSM = null;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.transform.SetParent(this.transform);
            }
        }

        private void OnCollisionExit2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                other.transform.SetParent(null);
            }
        }
    }
}