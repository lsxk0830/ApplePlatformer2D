using BehaviorDesigner.Runtime;
using QFramework;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Blue
{
    public class RemainSecondsEffectPlayer : MonoBehaviour
    {
        /// <summary>
        /// 寿命变更效果
        /// </summary>
        public GameObject RemainSecondsEffect;

        private void Start()
        {
            Bonfire.OnRemainSecondsChanged.Register(changeSeconds =>
            {
                var newEffect = Instantiate(RemainSecondsEffect, RemainSecondsEffect.transform.position,
                                                                RemainSecondsEffect.transform.rotation);
                newEffect.SetActive(true); // 显示
                newEffect.GetComponentInChildren<Text>().text = (changeSeconds < 0 ? "" : "+") + changeSeconds + "s"; // 设置文本
                newEffect.GetComponentInChildren<BehaviorTree>().EnableBehavior(); // 播放动画
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}