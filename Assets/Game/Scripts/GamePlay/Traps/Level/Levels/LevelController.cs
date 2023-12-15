using QFramework;
using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 关卡相关的控制脚本，暂时都让这个脚本静态部分负责
    /// </summary>
    public abstract class LevelController : MonoBehaviour
    {
        /// <summary>
        /// 这个事件一般与 LevelPass 脚本配合 （目前是LevelPassV2 这个prefab）
        /// </summary>
        public static EasyEvent OnCurrentLevelPassed = new EasyEvent();

        public static void PassCurrentLevel()
        {
            OnCurrentLevelPassed?.Trigger();
        }

        public static string CurrentLevelRuleName => mCurrentLevelRuleName;
        private static string mCurrentLevelRuleName=string.Empty;

        /// <summary>
        /// 暂定 levelRuleName 和 levelName一致
        /// </summary>
        /// <param name="levelRuleName"></param>
        public static void LoadLevel(string levelRuleName)
        {
            mCurrentLevelRuleName = levelRuleName;
        }

    }
}