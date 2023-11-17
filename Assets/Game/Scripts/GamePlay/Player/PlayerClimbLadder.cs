using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 爬梯子
    /// </summary>
    public class PlayerClimbLadder : MonoBehaviour
    {
        private bool mCanClimb = false;

        public enum States
        {
            NotClimb,
            Climb
        }

        private FSM<States> mFSM = new FSM<States>();

        private IInputSystem mInputSystem;

        private void Awake()
        {
            mInputSystem = ApplePlatformer2D.Interface.GetSystem<IInputSystem>();

            var playerMovement = GetComponent<PlayerMovement>();
            var rigidBody = GetComponent<Rigidbody2D>();
            var gravityScale = rigidBody.gravityScale;

            mFSM.State(States.NotClimb)
            .OnEnter(() =>
            {
                playerMovement.enabled = true;
                rigidBody.gravityScale = gravityScale;
            })
            .OnUpdate(() =>
            {
                if (mCanClimb && mInputSystem.VerticalInput != 0)
                {
                    mFSM.ChangeState(States.Climb);
                }
            })
            ;

            mFSM.State(States.Climb)
            .OnEnter(() =>
            {
                playerMovement.enabled = false;
                rigidBody.gravityScale = 0;
            })
            .OnUpdate(() =>
            {
                var verticalMovement = mInputSystem.VerticalInput;
                var horizontalMovement = mInputSystem.HorizontalInput;
                rigidBody.velocity = new Vector2(horizontalMovement, verticalMovement) * 5;
            });

            mFSM.StartState(States.NotClimb);
        }

        private void Update()
        {
            mFSM.Update();
        }

        private void OnDestroy()
        {
            mInputSystem = null;
            mFSM.Clear();
            mFSM = null;
        }

        public void CanClimb()
        {
            mCanClimb = true;
        }

        public void CantClimb()
        {
            mFSM.StartState(States.NotClimb);
            mCanClimb = false;
        }
    }
}
