using UnityEngine;
using QFramework;
using UnityEngine.Events;

namespace UTGM
{
    public class OnBonfireRuleUnlockedEventListener : MonoBehaviour
    {
        public string Key;

        public UnityEvent OnUnlock = new UnityEvent();
        private void Start()
        {
            var rule = ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(Key);

            if (rule.Unlocked)
            {
                OnUnlock?.Invoke();
            }

            ApplePlatformer2D.OnBonfireRuleUnlocked.Register((key) => 
            {
                if(key == Key)
                {
                    OnUnlock?.Invoke(); 
                }
            }).UnRegisterWhenGameObjectDestroyed(gameObject);
        }
    }
}