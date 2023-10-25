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
            // 第1关
            var level1 = new Level1()
                            .SecondsCost(10)
                            .Condition(self => !self.Passed)
                            .AddToRules(Rules);

            // 第一关结束后给一些奖励
            new HPBar()
                .SecondsCost(10)
                .Condition(_ => level1.Passed)
                .AddToRules(Rules);

            new MaxHPPlus1()
                .SecondsCost(10)
                .Condition(_ => level1.Passed)
                .AddToRules(Rules);

            // 第2关
            var level2 = new Level2()
                            .SecondsCost(10)
                            .Condition(self => !self.Passed && level1.Passed)
                            .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level2")
                .SecondsCost(10)
                .Condition(_ => level2.Passed)
                .AddToRules(Rules);

            // 第3关
            var level3 = new Level3()
                            .SecondsCost(10)
                            .Condition(self => !self.Passed && level2.Passed)
                            .AddToRules(Rules);

            new BonfireOpenUIRecoverHP()
                            .SecondsCost(10)
                            .Condition(_ => level3.Passed)
                            .AddToRules(Rules);

            // 第4关
            var level4 = new Level4()
                            .SecondsCost(10)
                            .Condition(self => !self.Passed && level3.Passed)
                            .AddToRules(Rules);

            new BonfireOpenUIRebornEnemy()
                            .SecondsCost(20)
                            .Condition(_ => level4.Passed)
                            .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level4")
                .SecondsCost(30)
                .Condition(_ => level4.Passed)
                .AddToRules(Rules);

            // 第5关
            var level5 = new Level5()
                            .SecondsCost(10)
                            .Condition(self => !self.Passed && level4.Passed)
                            .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level5")
                .SecondsCost(100)
                .Condition(_ => level5.Passed)
                .AddToRules(Rules);

            // 第6关
            var level6 = new Level6()
                            .SecondsCost(10)
                            .Condition(self => !self.Passed && level5.Passed)
                            .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level6")
                .SecondsCost(120)
                .Condition(_ => level6.Passed)
                .AddToRules(Rules);

            // 第7关
            var level7 = new Level7()
                            .SecondsCost(10)
                            .Condition(self => !self.Passed && level6.Passed)
                            .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level7")
                .SecondsCost(120)
                .Condition(_ => level7.Passed)
                .AddToRules(Rules);

            // 第8关
            var level8 = new Level8()
                            .SecondsCost(10)
                            .Condition(self => !self.Passed && level7.Passed)
                            .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level8")
                .SecondsCost(120)
                .Condition(_ => level8.Passed)
                .AddToRules(Rules);

            new DoubleJumpRule()
                .SecondsCost(30)
                .Condition(_ => level8.Passed)
                .AddToRules(Rules);
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