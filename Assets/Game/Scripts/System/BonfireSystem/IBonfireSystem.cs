using System.Collections.Generic;
using QFramework;

namespace Blue
{
    public interface IBonfireSystem : ISystem
    {
        List<IBonfireRule> Rules { get; }

        IBonfireRule GetRuleByKey(string key);
    }

    public class BonfireSystem : AbstractSystem, IBonfireSystem
    {
        public List<IBonfireRule> Rules { get; } = new List<IBonfireRule>();

        protected override void OnInit()
        {
            // 添加规则
            Rules.Add(new HPBar());
            Rules.Add(new MaxHPPlus1());
            Rules.Add(new BonfireOpenUIRebornEnemy());
            Rules.Add(new BonfireOpenUIRecoverHP());
            Rules.Add(new Level1());
            Rules.Add(new Level2());
            Rules.Add(new Level3());
            Rules.Add(new Level4());
            Rules.Add(new Level5());
            Rules.Add(new Level6());
            Rules.Add(new Level7());
            Rules.Add(new Level8());
            Rules.Add(new DoubleJumpRule());
        }

        public IBonfireRule GetRuleByKey(string key)
        {
            foreach (var bonfireRule in Rules)
            {
                if (bonfireRule.Key == key)
                {
                    return bonfireRule;
                }
            }
            return null;
        }
    }
}