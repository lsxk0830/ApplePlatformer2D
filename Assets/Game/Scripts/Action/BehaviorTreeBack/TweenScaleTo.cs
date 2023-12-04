using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Blue
{
    public class TweenScaleTo : Action
    {
        public SharedTransform Target;
        public SharedVector3 ScaleTo;
        public SharedFloat Duration;

        private float mCurrentSeconds;
        private Vector3 mOriginScale;

        public override void OnStart()
        {
            mOriginScale = Target.Value.localScale;
            mCurrentSeconds = 0;
        }

        public override TaskStatus OnUpdate()
        {
            mCurrentSeconds += Time.deltaTime;
            Target.Value.localScale = Vector3.Lerp(mOriginScale,ScaleTo.Value,mCurrentSeconds/Duration.Value);

            if(mCurrentSeconds>=Duration.Value)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Running;
        }
    }
}