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
            GUILayout.Label("HPBar");

            if(Unlocked)
            {

            }
            else
            {
                if(GUILayout.Button("解锁"))
                {
                    Unlocked = true;
                }
            }
        }

        public void OnGUI()
        {
            if(Unlocked)
            {
                GUILayout.BeginArea(new Rect(Screen.width-200,0,200,200)); // 在一个固定的屏幕区域中开始 GUI 控件的 GUILayout 块
                GUILayout.Label("血量:1/1");
                GUILayout.EndArea();
            }
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