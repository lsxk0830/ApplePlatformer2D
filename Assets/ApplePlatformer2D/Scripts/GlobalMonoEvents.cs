using QFramework;
using UnityEngine;

namespace UTGM
{
    public class GlobalMonoEvents : MonoBehaviour
    {
        public static EasyEvent OnApplicationQuitEvent = new EasyEvent();

        private void OnApplicationQuit()
        {
            OnApplicationQuitEvent?.Trigger();
        }

        // 允许在运行时加载游戏时不通过用户操作 初始化一个运行时类方法
        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            var gameObj = new GameObject("GlobalMonoEvents");

            gameObj.AddComponent<GlobalMonoEvents>();

            DontDestroyOnLoad(gameObj);
        }
    }
}