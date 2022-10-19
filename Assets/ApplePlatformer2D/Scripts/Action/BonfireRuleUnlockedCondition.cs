using UnityEngine;
using UnityEngine.Events;

namespace UTGM
{
    public class BonfireRuleUnlockedCondition : MonoBehaviour
    {
        public UnityEvent IsUnlocked = new UnityEvent();

        public string Key;

        public void Execute()
        {
            var rule = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(Key);

            if (rule.Unlocked)
            {
                IsUnlocked?.Invoke();
            }
        }
    }
}