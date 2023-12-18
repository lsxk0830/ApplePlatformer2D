using System;
using System.Collections.Generic;
using QFramework;

namespace Blue
{
    public interface IBonfireSystem : ISystem
    {
        List<IBonfireRule> Rules { get; }
        void GenerateRandomLevel();
        IBonfireRule GetRuleByKey(string key);
    }

    public class BonfireSystem : AbstractSystem, IBonfireSystem
    {
        public List<IBonfireRule> Rules { get; } = new List<IBonfireRule>();

        string GetRandomLevelIndexName(List<string> levels)
        {
            int index = UnityEngine.Random.Range(0, levels.Count); // 随机 0 ~ 关卡数量
            var levelIndex = levels[index]; // 缓存第几关
            levels.RemoveAt(index); // 删除掉对应数字
            return levelIndex;
        }
        protected override void OnInit()
        {
            GenerateRandomLevel();
        }

        /// <summary>
        /// 对外提供一个随机生成Level的功能
        /// </summary>
        public void GenerateRandomLevel()
        {
            Rules.Clear();
            var stage1 = new List<string>() // 未生成的关卡，第九关需要二段跳，特殊处理
            {
                "LevelR1"
            };
            var stage2 = new List<string>() // 未生成的关卡，第九关需要二段跳，特殊处理
            {
                "Level1","Level2","Level3"
            };
            var stage3 = new List<string>() // 未生成的关卡，第九关需要二段跳，特殊处理
            {
                "Level4","Level5","Level6"
            };
            var stage4 = new List<string>() // 未生成的关卡，第九关需要二段跳，特殊处理
            {
                "Level7","Level8","Level9"
            };

            var dash = new DashRule()
                //.SecondsCost(1) // 测试
                .AddToRules(Rules);
            var roll = new RollRule()
                //.SecondsCost(1) // 测试
                .AddToRules(Rules);

            // 第1关
            var level1 = new GenericlLevel()
                .WithKey(GetRandomLevelIndexName(stage1))
                .WithDisplayName("第1关")
                .SecondsCost(10)
                .Condition(self => !self.Passed)
                .AddToRules(Rules);
            var hpBar = new HPBar()
                .SecondsCost(5)
                .Condition(_ => level1.Passed)
                .AddToRules(Rules);
            new MaxHPPlus1()
                .SecondsCost(5)
                .Condition(_ => hpBar.Unlocked)
                .AddToRules(Rules);

            // 第2关
            var level2 = new GenericlLevel()
                .WithKey(GetRandomLevelIndexName(stage2))
                .WithDisplayName("第2关")
                .SecondsCost(10)
                .Condition(self => !self.Passed && level1.Passed)
                .AddToRules(Rules);
            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level2")
                .SecondsCost(10)
                .Condition(_ => level2.Passed)
                .AddToRules(Rules);
            new BonfireOpenUIRecoverHP()
                .SecondsCost(10)
                .Condition(_ => level2.Passed)
                .AddToRules(Rules);
            new DoubleJumpRule()
                .SecondsCost(100)
                .Condition(self => level2.Passed)
                .AddToRules(Rules);
            new BonfireOpenUIRebornEnemy()
                .SecondsCost(20)
                .Condition(_ => level2.Passed)
                .AddToRules(Rules);
            new AddHPEvery10ApplesRule()
                .SecondsCost(100)
                .Condition(_ => level2.Passed)
                .AddToRules(Rules);

            // 第3关
            var level3 = new GenericlLevel()
                .WithKey(GetRandomLevelIndexName(stage3))
                .WithDisplayName("第3关")
                .SecondsCost(10)
                .Condition(self => !self.Passed && level2.Passed)
                .AddToRules(Rules);
            new MaxHPPlus1()
               .WithKey("MaxHPPlus1_Level3")
               .SecondsCost(30)
               .Condition(_ => level3.Passed)
               .AddToRules(Rules);

            // 第4关
            var level4 = new GenericlLevel()
                .WithKey(GetRandomLevelIndexName(stage4))
                .WithDisplayName("第4关")
                .SecondsCost(10)
                .Condition(self => !self.Passed && level3.Passed)
                .AddToRules(Rules);

            new PassAllLevel()
                .Condition(_ => level4.Passed)
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