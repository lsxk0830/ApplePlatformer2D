using System;
using UnityEngine;

namespace Blue
{
    public class MaxHPPlus1 : IBonfireRule
    {
        public int NeedSeconds { get; } = 10;
        public string Key { get; } = nameof(MaxHPPlus1);
        public bool Unlocked { get; private set; }
        Lazy<IBonfireRule> mHPBarRule = new Lazy<IBonfireRule>(() =>
            ApplePlatformer2D.Interface.GetSystem<IBonfireSystem>().GetRuleByKey(nameof(HPBar)));

        public void OnBonfireOnGUI()
        {
            // 未解锁时
            if (!Unlocked && mHPBarRule.Value.Unlocked) // HPBar 解锁时
            {
                GUILayout.BeginHorizontal(); // 开始一个水平控件组
                GUILayout.Label("最大HP+1", Styles.label.Value);
                GUILayout.Label("价格:" + NeedSeconds, Styles.label.Value);

                GUILayout.FlexibleSpace(); // 插入灵活的空白元素

                if (Bonfire.RemainSeconds > NeedSeconds)
                {
                    if (GUILayout.Button("解锁", Styles.Button.Value))
                    {
                        Bonfire.RemainSeconds -= NeedSeconds;

                        var playerModel = ApplePlatformer2D.Interface.GetModel<IPlayerModel>();
                        playerModel.HP++;
                        playerModel.MaxHP++;

                        Unlocked = true;
                    }
                }
                else
                {
                    GUILayout.Label("时间不足", Styles.label.Value);
                }
                GUILayout.EndHorizontal();
            }
        }

        public void OnGUI()
        {

        }

        public void OnTopRightGUI()
        {

        }

        public void Reset()
        {
            Unlocked = false;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(nameof(MaxHPPlus1), Unlocked ? 1 : 0);
        }

        public IBonfireRule Load()
        {
            Unlocked = PlayerPrefs.GetInt(nameof(MaxHPPlus1), 0) == 1;
            return this;
        }
    }
}