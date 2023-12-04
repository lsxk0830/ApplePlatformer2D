using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace Blue
{
    public class TweenTextAlpha : Action
    {
        public Text Target;
        public SharedFloat ToAlpha;
        public SharedFloat Duration;
        private float mCurrentSeconds;
        private float mOriginAlpha;

        public override void OnStart()
        {
            mOriginAlpha = Target.color.a;
            mCurrentSeconds = 0;
        }

        public override TaskStatus OnUpdate()
        {
            mCurrentSeconds += Time.deltaTime;
            var currentAlpha = Mathf.Lerp(mOriginAlpha, ToAlpha.Value, mCurrentSeconds / Duration.Value);
            var color = Target.color;
            color.a = currentAlpha;
            Target.color = color;
            return mCurrentSeconds >= Duration.Value ? TaskStatus.Success : TaskStatus.Running;
        }
    }
}