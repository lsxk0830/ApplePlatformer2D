using UnityEngine;

namespace Blue
{
    public class Level4 : IBonfireRule
    {
        public int NeedSeconds { get; } = 30;
        public string Key { get; } = nameof(Level4);
        public bool Unlocked { get; private set; }

        public void Reset()
        {
            Unlocked = false;
        }
        public void OnBonfireOnGUI()
        {
            if (!Unlocked)
            {
                GUILayout.BeginHorizontal(); // 开始一个水平控件组
                GUILayout.Label("第四关", Styles.label.Value);
                GUILayout.Label("价格:" + NeedSeconds, Styles.label.Value);

                GUILayout.FlexibleSpace(); // 插入灵活的空白元素

                if (Bonfire.RemainSeconds > NeedSeconds)
                {
                    if (GUILayout.Button("解锁", Styles.Button.Value))
                    {
                        Bonfire.RemainSeconds -= NeedSeconds;
                        ApplePlatformer2D.OnBonfireRuleUnlocked.Trigger(Key);
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

        public void Save()
        {
            PlayerPrefs.SetInt(nameof(Level4), Unlocked ? 1 : 0);
        }

        public IBonfireRule Load()
        {
            Unlocked = PlayerPrefs.GetInt(nameof(Level4), 0) == 1;
            return this;
        }
    }
}