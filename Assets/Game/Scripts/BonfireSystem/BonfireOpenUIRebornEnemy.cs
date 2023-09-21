using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 打开火堆UI重新生成敌人
    /// </summary>
    public class BonfireOpenUIRebornEnemy : IBonfireRule
    {
        public int NeedSeconds { get; } = 5;

        public string Key { get; } = nameof(BonfireOpenUIRebornEnemy);

        public bool Unlocked { get;private set; }


        public void OnBonfireOnGUI()
        {
            // 未解锁时
            if (!Unlocked)
            {
                GUILayout.BeginHorizontal(); // 开始一个水平控件组
                GUILayout.Label("在火堆重生敌人", Styles.label.Value);
                GUILayout.Label("价格:" + NeedSeconds, Styles.label.Value);

                GUILayout.FlexibleSpace(); // 插入灵活的空白元素

                if (Bonfire.RemainSeconds > NeedSeconds)
                {
                    if (GUILayout.Button("解锁", Styles.Button.Value))
                    {
                        Bonfire.RemainSeconds -= NeedSeconds;
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
        }
        public void Reset()
        {
            Unlocked = false;
        }

        public IBonfireRule Load()
        {
            return this;
        }
    }
}