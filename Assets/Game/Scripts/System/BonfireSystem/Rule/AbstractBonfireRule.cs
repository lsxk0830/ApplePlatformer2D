using UnityEngine;

namespace Blue
{
    public abstract class AbstractBonfireRule : IBonfireRule
    {
        public abstract int NeedSeconds { get; }
        public abstract string Key { get; }
        public bool Unlocked { get; protected set; }
        public abstract string DisplayName { get; }

        public void Reset()
        {
            Unlocked = false;
        }

        public virtual void OnBonfireOnGUI()
        {
            if (!Unlocked)
            {
                GUILayout.BeginHorizontal(); // 开始一个水平控件组
                GUILayout.Label(DisplayName, Styles.label.Value);
                GUILayout.Label("价格:" + NeedSeconds, Styles.label.Value);

                GUILayout.FlexibleSpace(); // 插入灵活的空白元素

                if (Bonfire.RemainSeconds > NeedSeconds)
                {
                    if (GUILayout.Button("解锁", Styles.Button.Value))
                    {
                        Bonfire.RemainSeconds -= NeedSeconds;

                        OnUnlock();

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
        }

        public IBonfireRule Load()
        {
            Unlocked = PlayerPrefs.GetInt(Key, 0) == 1;
            return this;
        }
    }
}