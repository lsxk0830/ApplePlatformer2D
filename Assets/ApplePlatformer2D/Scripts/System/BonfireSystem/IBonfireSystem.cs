using QFramework;
using System.Collections.Generic;

namespace UTGM
{
    public interface IBonfireSystem : ISystem
    {
        List<IBonfireRule> Rules { get; }

        IBonfireRule GetRuleByKey(string ruleKey);
    }

    public class BonfireSystem : AbstractSystem, IBonfireSystem
    {
        public List<IBonfireRule> Rules { get; } = new List<IBonfireRule> ();

        protected override void OnInit()
        {
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
        }
     
        public IBonfireRule GetRuleByKey(string ruleKey)
        {
            foreach (var bonfireRule in Rules)
            {
                if (bonfireRule.Key == ruleKey)
                {
                    return bonfireRule;
                }
            }
            return null;
        }
    }
}