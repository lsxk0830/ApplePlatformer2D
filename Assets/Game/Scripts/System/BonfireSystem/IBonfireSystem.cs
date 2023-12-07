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

        int GetRandomLevelIndex(List<int> levels)
        {
            int index = UnityEngine.Random.Range(0, levels.Count); // 随机 0 ~ 关卡数量
            int levelIndex = levels[index]; // 缓存第几关
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
            List<int> ungenratedLevels = new List<int>() // 未生成的关卡，第九关需要二段跳，特殊处理
            {
                1,2,3,4,5,6,7,8
            };

            // 第1关
            var level1 = new GenericlLevel()
                            .WithKey("Level" + GetRandomLevelIndex(ungenratedLevels))
                            .WithDisplayName("第?关")
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
            var level2 = new GenericlLevel()
                            .WithKey("Level" + GetRandomLevelIndex(ungenratedLevels))
                            .WithDisplayName("第?关")
                            .SecondsCost(10)
                            .Condition(self => !self.Passed && level1.Passed)
                            .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level2")
                .SecondsCost(10)
                .Condition(_ => level2.Passed)
                .AddToRules(Rules);

            // 第3关
            var level3 = new GenericlLevel()
                            .WithKey("Level" + GetRandomLevelIndex(ungenratedLevels))
                            .WithDisplayName("第?关")
                            .SecondsCost(10)
                            .Condition(self => !self.Passed && level2.Passed)
                            .AddToRules(Rules);

            new BonfireOpenUIRecoverHP()
                            .SecondsCost(10)
                            .Condition(_ => level3.Passed)
                            .AddToRules(Rules);

            // 第4关
            var level4 = new GenericlLevel()
                            .WithKey("Level" + GetRandomLevelIndex(ungenratedLevels))
                            .WithDisplayName("第?关")
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
            var level5 = new GenericlLevel()
                            .WithKey("Level" + GetRandomLevelIndex(ungenratedLevels))
                            .WithDisplayName("第?关")
                            .SecondsCost(10)
                            .Condition(self => !self.Passed && level4.Passed)
                            .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level5")
                .SecondsCost(100)
                .Condition(_ => level5.Passed)
                .AddToRules(Rules);

            // 第6关
            var level6 = new GenericlLevel()
                            .WithKey("Level" + GetRandomLevelIndex(ungenratedLevels))
                            .WithDisplayName("第?关")
                            .SecondsCost(10)
                            .Condition(self => !self.Passed && level5.Passed)
                            .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level6")
                .SecondsCost(120)
                .Condition(_ => level6.Passed)
                .AddToRules(Rules);

            new AddHPEvery10ApplesRule()
                .Condition(_ => level6.Passed)
                .AddToRules(Rules);

            // 第7关
            var level7 = new GenericlLevel()
                            .WithKey("Level" + GetRandomLevelIndex(ungenratedLevels))
                            .WithDisplayName("第?关")
                            .SecondsCost(10)
                            .Condition(self => !self.Passed && level6.Passed)
                            .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level7")
                .SecondsCost(120)
                .Condition(_ => level7.Passed)
                .AddToRules(Rules);

            // 第8关
            var level8 = new GenericlLevel()
                            .WithKey("Level" + GetRandomLevelIndex(ungenratedLevels))
                            .WithDisplayName("第?关")
                            .SecondsCost(10)
                            .Condition(self => !self.Passed && level7.Passed)
                            .AddToRules(Rules);

            new MaxHPPlus1()
                .WithKey("MaxHPPlus1_Level8")
                .SecondsCost(120)
                .Condition(_ => level8.Passed)
                .AddToRules(Rules);

            var doubleJump = new DoubleJumpRule()
                .SecondsCost(30)
                .Condition(_ => level8.Passed)
                .AddToRules(Rules);

            // 第9关
            var level9 = new GenericlLevel()
                            .WithKey("Level9")
                            .WithDisplayName("第9关")
                            .SecondsCost(10)
                            .Condition(self => !self.Passed && level8.Passed)
                            .AddToRules(Rules);

            // 每次开始一局新的游戏时，关卡可以重新排列一下

            // 第九个通过就通关
            new PassAllLevel()
                .Condition(_ => level9.Passed)
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