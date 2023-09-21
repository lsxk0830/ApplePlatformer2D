using UnityEngine;
using UnityEngine.SceneManagement;

namespace Blue
{
    /// <summary>
    /// 重新加载场景
    /// </summary>
    public class ReloadLevel : MonoBehaviour
    {
        public void Execute()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}