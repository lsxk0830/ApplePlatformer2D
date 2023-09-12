using UnityEngine;
using UnityEngine.SceneManagement;

namespace Blue
{
    /// <summary>
    /// 死亡区域
    /// </summary>
    public class DeathArea : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D other)
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}