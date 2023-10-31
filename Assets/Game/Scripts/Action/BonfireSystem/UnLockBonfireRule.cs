using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 重新解锁规则，如关卡
    /// </summary>
    public class UnLockBonfireRule : MonoBehaviour
    {
        public string RuleName;
        public void Execute()
        {
            var rule = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(RuleName);
            rule.Unlock();
        }
    }
}