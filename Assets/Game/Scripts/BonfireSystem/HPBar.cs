using System;
using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 血量条
    /// </summary>
    public class HPBar : IBonfireRule
    {
        Lazy<IPlayerModel> mPlayerModel = new Lazy<IPlayerModel>(() => ApplePlatformer2D.Interface.GetModel<IPlayerModel>());
        public int NeedSeconds { get; } = 30;

        public string Key { get; } = nameof(HPBar);

        public bool Unlocked { get; private set; }

        public void OnBonfireOnGUI()
        {
            // 未解锁时
            if (!Unlocked)
            {
                GUILayout.BeginHorizontal(); // 开始一个水平控件组
                GUILayout.Label("血量条", Styles.label.Value);
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

        public void OnTopRightGUI()
        {
            if (Unlocked)
            {
                GUILayout.Label($"血量:{mPlayerModel.Value.HP}/{mPlayerModel.Value.MaxHP}", Styles.label.Value);
            }
        }

        public void OnGUI()
        {

        }

        public void Reset()
        {
            Unlocked = false;
        }
        public void Save()
        {
            PlayerPrefs.SetInt(nameof(HPBar), Unlocked ? 1 : 0);
        }
        public IBonfireRule Load()
        {
            Unlocked = PlayerPrefs.GetInt(nameof(HPBar), 0) == 1;
            return this;
        }

    }
}