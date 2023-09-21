using System;
using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 显示UI字体的样式
    /// </summary>
    public class Styles
    {
        public static Lazy<GUIStyle> label = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
        {
            fontSize = 20,
            //font = mFont.Value,
        });

        public static Lazy<GUIStyle> Button = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button)
        {
            fontSize = 20,
            //font = mFont.Value,
        });
        public static Lazy<GUIStyle> Biglabel = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.label)
        {
            fontSize = 30,
            //font = mFont.Value,
        });

        public static Lazy<GUIStyle> BigButton = new Lazy<GUIStyle>(() => new GUIStyle(GUI.skin.button)
        {
            fontSize = 30,
            //font = mFont.Value,
        });
    }
}