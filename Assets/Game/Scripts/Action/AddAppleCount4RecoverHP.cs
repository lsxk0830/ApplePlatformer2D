using UnityEngine;
using UnityEngine.Events;

namespace Blue
{
    /// <summary>
    /// 尝试为了恢复主角HP
    /// </summary>
    public class AddAppleCount4RecoverHP : MonoBehaviour
    {
        public UnityEvent OnHPAdd = new UnityEvent();

        public void Execute()
        {
            var rule = ApplePlatformer2D.Interface
            .GetSystem<IBonfireSystem>()
            .GetRuleByKey(nameof(AddHPEvery10ApplesRule));

            if(rule.Unlocked)
            {
                var playerModel = ApplePlatformer2D.Interface
                .GetModel<IPlayerModel>();
                playerModel.CurrentAppleCount++;
                if(playerModel.CurrentAppleCount>=10)
                {
                    playerModel.CurrentAppleCount -= 10;
                    if(playerModel.HP<playerModel.MaxHP)
                    {
                        playerModel.HP++;
                        OnHPAdd?.Invoke();
                    }
                }
            }
        }
    }
}