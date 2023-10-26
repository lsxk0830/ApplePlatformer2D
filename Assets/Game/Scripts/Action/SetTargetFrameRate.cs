using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 目标帧率
    /// </summary>
    public class SetTargetFrameRate : MonoBehaviour
    {
        public int FPS = 60;

        public void Execute()
        {
            Application.targetFrameRate = FPS;
        }
    }
}