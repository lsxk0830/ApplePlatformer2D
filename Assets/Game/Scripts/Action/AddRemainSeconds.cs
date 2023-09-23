using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 增加剩余时间
    /// </summary>
    public class AddRemainSeconds : MonoBehaviour
    {
        public void Execute(int seconds)
        {
            Bonfire.RemainSeconds += seconds;
        }
    }
}