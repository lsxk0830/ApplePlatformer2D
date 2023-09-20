using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 血量条
    /// </summary>
    public class HPBar : IBonfireRule
    {
        public string Key { get; }

        public bool Unlocked { get; private set; }

        public void OnBonfireOnGUI()
        {
            // 未解锁时
            if(!Unlocked)
            {
                GUILayout.BeginHorizontal(); // 开始一个水平控件组
                GUILayout.Label("HPBar");

                if(GUILayout.Button("解锁"))
                {
                    Unlocked = true;
                }
                GUILayout.EndHorizontal();
            }
        }

        public void OnTopRightGUI()
        {
            if(Unlocked)
            {
                GUILayout.Label("血量:1/1");
            }
        }

        public void OnGUI()
        {
            
        }

        public void Save()
        {

        }
        public IBonfireRule Load()
        {
            return this;
        }

    }
}