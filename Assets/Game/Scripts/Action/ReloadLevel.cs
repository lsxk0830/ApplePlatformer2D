using UnityEngine;
using UnityEngine.SceneManagement;

namespace Blue
{
    public class ReloadLevel : MonoBehaviour
    {
        public void Execute()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}