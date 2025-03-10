using System.Collections.Generic;
using System;
using UnityEngine;

namespace Blue
{
    public abstract class AbstractBonfireRule : IBonfireRule
    {
        public Func<AbstractBonfireRule, bool> VisibleCondition { get; set; } = _ => true;

        public abstract int NeedSeconds { get; set; }
        public abstract string Key { get; set; }
        public bool Unlocked { get; set; }
        public abstract string DisplayName { get; set; }

        public void Unlock()
        {
            Bonfire.SetRemainSecondsWithChangerEvent(Bonfire.RemainSeconds - NeedSeconds);
            AudioSystem.PlayUIFeedback();

            OnUnlock();
            Unlocked = true;
        }

        public void Reset()
        {
            Unlocked = false;
            OnReset();
        }
        protected virtual void OnReset() { }

        public virtual void OnBonfireOnGUI()
        {
            //Debug.Log($"this:{this.DisplayName},mVisibleCondition:{!mVisibleCondition(this)}");
            if (!VisibleCondition(this)) return;

            if (!Unlocked)
            {
                GUILayout.BeginHorizontal(); // 开始一个水平控件组
                GUILayout.Label(DisplayName, Styles.label.Value);
                GUILayout.Label("价格:" + NeedSeconds + "s", Styles.label.Value);

                GUILayout.FlexibleSpace(); // 插入灵活的空白元素

                if (Bonfire.RemainSeconds > NeedSeconds)
                {
                    if (GUILayout.Button("解锁", Styles.Button.Value))
                    {
                        Unlock();
                    }
                }
                else
                {
                    GUILayout.Label("时间不足", Styles.label.Value);
                }
                GUILayout.EndHorizontal();
            }
        }

        /// <summary>
        /// 例：第一关解锁后，打开第一关窗口
        /// </summary>
        protected virtual void OnUnlock()
        {

        }

        public void OnGUI()
        {

        }

        public virtual void OnTopRightGUI()
        {

        }

        public void Save()
        {
            PlayerPrefs.SetInt(Key, Unlocked ? 1 : 0);
            OnSave();
        }
        protected virtual void OnSave() { }

        public IBonfireRule Load()
        {
            Unlocked = PlayerPrefs.GetInt(Key, 0) == 1;
            OnLoad();
            return this;
        }
        protected virtual void OnLoad() { }
    }

    public static class AbstractBonfireRuleExtension
    {
        /// <summary>
        /// 显示的名称
        /// </summary>
        public static T WithDisplayName<T>(this T self, string displayName) where T : AbstractBonfireRule
        {
            self.DisplayName = displayName;
            return self;
        }

        /// <summary>
        /// 设置 Key
        /// </summary>
        public static T WithKey<T>(this T self, string key) where T : AbstractBonfireRule
        {
            self.Key = key;
            return self;
        }

        /// <summary>
        /// 花费的秒数
        /// </summary>
        public static T SecondsCost<T>(this T self, int needSeconds) where T : AbstractBonfireRule
        {
            self.NeedSeconds = needSeconds;
            return self;
        }

        /// <summary>
        /// 状态
        /// </summary>
        public static T Condition<T>(this T self, Func<T, bool> visibleCondition) where T : AbstractBonfireRule
        {
            self.VisibleCondition = _ => visibleCondition(self);
            return self;
        }

        /// <summary>
        /// 添加到规则
        /// </summary>
        public static T AddToRules<T>(this T self, List<IBonfireRule> rules) where T : IBonfireRule
        {
            rules.Add(self);
            return self;
        }
    }
}