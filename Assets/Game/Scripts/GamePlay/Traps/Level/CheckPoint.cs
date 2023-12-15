using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 检查点
    /// </summary>
    public class CheckPoint : MonoBehaviour
    {
        // 用于记录当前检查点的静态变量
        public static CheckPoint CurrentCheckPoint { get; private set; }

        // 对外提供一个方法，用于将主角送回检查点
        public static void SendPlayerToLastCheckPoint()
        {
            var player = GameObject.FindWithTag("Player");

            if (CurrentCheckPoint)
                player.transform.position = CurrentCheckPoint.transform.position;
            else
                player.transform.position = Vector3.zero;
        }

        // 提供一个清空当前检查点的方法
        public static void ClearCheckPoint()
        {
            CurrentCheckPoint = null;
        }

        // 监听主角进入
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                // 记录当前的检查点
                CurrentCheckPoint = this;
            }
        }

        // 跳转场景清空检查点记录
        private void OnDestroy()
        {
            if(CurrentCheckPoint)
                ClearCheckPoint();
        }
    }
}