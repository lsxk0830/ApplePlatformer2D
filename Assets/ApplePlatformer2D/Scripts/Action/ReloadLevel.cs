using UnityEngine;
using UnityEngine.SceneManagement;
public class ReloadLevel : MonoBehaviour
{
    public void Execute()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
