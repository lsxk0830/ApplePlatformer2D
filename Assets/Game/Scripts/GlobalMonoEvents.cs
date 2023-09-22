using QFramework;
using UnityEngine;

namespace Blue
{
    public class GlobalMonoEvents : MonoBehaviour
    {
        public static EasyEvent OnApplicationQuitEvent = new EasyEvent();

        private void OnApplicationQuit()
        {
            OnApplicationQuitEvent?.Trigger();
        }

        [RuntimeInitializeOnLoadMethod]
        public static void Initialize()
        {
            var gameObj = new GameObject("GlobalMonoEvents");
            gameObj.AddComponent<GlobalMonoEvents>();
            DontDestroyOnLoad(gameObj);
        }
    }
}