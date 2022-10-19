using UnityEngine;

namespace UTGM
{
    public abstract class AbstractBonfireRule : IBonfireRule
    {
        public abstract int NeedSeconds { get; }
        public abstract string Key { get; }
        public abstract string DisplayName { get; }
        public bool Unlocked { get; protected set; }

        public virtual void OnBonfireGUI()
        {
            if (!Unlocked)
            {
                GUILayout.BeginHorizontal(); // 开始一个水平控件组

                GUILayout.Label(DisplayName, Styles.label.Value);
                GUILayout.Label("价格:" + NeedSeconds, Styles.label.Value);

                GUILayout.FlexibleSpace();// 插入灵活的空白元素。灵活的空白元素将占用布局中的任何剩余空间

                if (Bonfire.RemainSeconds > NeedSeconds)
                {
                    if (GUILayout.Button("解锁", Styles.Button.Value))
                    {
                        Bonfire.RemainSeconds -= NeedSeconds;

                        OnUnlock();
                        AudioSystem.PlayerUIFeedback();
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

        protected virtual void OnUnlock()
        {
        }

        public void OnGUI()
        {

        }

        public virtual void OnTopRightGUI()
        {

        }

        public void Reset()
        {
            Unlocked = false;
        }
        public IBonfireRule Load()
        {
            Unlocked = PlayerPrefs.GetInt(Key, 0) == 1;
            return this;
        }

        public void Save()
        {
            PlayerPrefs.SetInt(Key, Unlocked ? 1 : 0);
        }
    }

}
