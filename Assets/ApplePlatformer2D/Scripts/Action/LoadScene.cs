using UnityEngine;
using UnityEngine.SceneManagement;

namespace UTGM
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
