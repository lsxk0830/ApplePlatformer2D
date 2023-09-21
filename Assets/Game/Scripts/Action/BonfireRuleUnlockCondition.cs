using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    /// <summary>
    /// 火堆规则解锁状态
    /// </summary>
    public class BonfireRuleUnlockCondition : MonoBehaviour
    {
        public UnityEvent IsUnlocked = new UnityEvent();
        public string Key;

        public void Execute()
        {
            var rule = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(Key);

            if(rule.Unlocked)
            {
                IsUnlocked?.Invoke();
            }
        }
    }
}