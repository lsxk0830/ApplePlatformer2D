using System.Collections.Generic;
using UnityEngine;
using QFramework;
using UnityEngine.Events;

namespace Blue
{
    /// <summary>
    /// 火堆解锁事件监听，例如解锁第四关
    /// </summary>
    public class OnBonfireRuleUnlockedEventListener : MonoBehaviour
    {
        public string Key;
        public List<string> Keys = new List<string>();
        public UnityEvent OnUnlock = new UnityEvent();

        private void Start()
        {
            Keys.Add(Key);
            foreach (var key in Keys)
            {
                var rule = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(Key);

                if (rule != null && rule.Unlocked)
                {
                    OnUnlock?.Invoke();
                }

                ApplePlatformer2D.OnBonfireRuleUnlocked.Register(key =>
                {
                    if (Keys.Contains(key))
                    {
                        OnUnlock?.Invoke();
                    }
                }).UnRegisterWhenGameObjectDestroyed(gameObject);
            }
        }
    }
}