using UnityEngine;

namespace Blue
{
    /// <summary>
    /// 设置当前物体父物体为null
    /// </summary>
    public class SetAsRootGameObject : MonoBehaviour
    {
        public void Execute()
        {
            transform.SetParent(null);
        }
    }
}