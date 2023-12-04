using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 移动Tween动画
    /// </summary>
    public class TweenMoveByLocal : Action
    {
        public SharedTransform Target; // 要移动的Transform
        public SharedVector3 MoveByLocal;
        public SharedFloat Duration; // 间隔时长
        private Vector3 mOriginLocalPosition; // 原来的位置
        private Vector3 mToLocalPosition; // 目标位置
        private float mCurrentSeconds;

        public override void OnStart()
        {
            mOriginLocalPosition = Target.Value.localPosition;
            mToLocalPosition = mOriginLocalPosition + MoveByLocal.Value;
            mCurrentSeconds = 0;
        }

        public override TaskStatus OnUpdate()
        {
            mCurrentSeconds += Time.deltaTime;
            //Debug.Log($"mCurrentSeconds:{mCurrentSeconds},值：{mCurrentSeconds / Duration.Value}");
            Target.Value.localPosition = Vector3.Lerp(mOriginLocalPosition, mToLocalPosition, mCurrentSeconds / Duration.Value);
            if (mCurrentSeconds >= Duration.Value)
            {
                return TaskStatus.Success;
            }
            return TaskStatus.Running;
        }
    }
}