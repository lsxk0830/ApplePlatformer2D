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

        public States state = States.Stop;
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

            mStopTime = Time.time;
        }

        private float mStopTime;
        private void Update()
        {
            if (state == States.Stop)
            {
                if (Time.time - mStopTime >= StopSeconds)
                {
                    if (mToPosition == mPos2)
                        mToPosition = mPos1;
                    else
                        mToPosition = mPos2;

                    state = States.Moving;
                }
            }
            else if (state == States.Moving)
            {
                var currentPosition = transform.position;
                var moveDirection = mToPosition - currentPosition; // 移动方向
                moveDirection = moveDirection.normalized; // 归一化
                transform.Translate(moveDirection * MovementSpeed * Time.deltaTime);
                if (Vector3.Distance(currentPosition, mToPosition) < 0.1f)
                {
                    mStopTime = Time.time;
                    state = States.Stop;
                }
            }
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