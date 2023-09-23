using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 销毁物体
    /// </summary>
    public class DestroyGameObject : MonoBehaviour
    {
        public void Execute()
        {
            Destroy(gameObject);
        }
    }
}