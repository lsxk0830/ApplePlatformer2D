using UnityEngine.SceneManagement;
using UnityEngine;

namespace Blue
{
    public class LoadScene : MonoBehaviour
    {
        public string SceneName;

        public void Execute()
        {
            SceneManager.LoadScene(SceneName);
        }
    }
}