using UnityEngine;
using UnityEngine.SceneManagement;

namespace Blue
{
    /// <summary>
    /// 保存隐藏状态
    /// </summary>
    public class SaveHideStateController : MonoBehaviour
    {
        private void Awake()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            var objName = name;
            var savedKey = sceneName + objName;

            var saveSystem = ApplePlatformer2D.Interface.GetSystem<ISaveSystem>();

            if(saveSystem.HasSavedKey(savedKey))
            {
                gameObject.SetActive(false);
            }
        }

        public void SaveHideState()
        {
            var sceneName = SceneManager.GetActiveScene().name;
            var objName = name;
            var savedKey = sceneName + objName;

            ApplePlatformer2D.Interface.GetSystem<ISaveSystem>().AddSaveKey(savedKey);
        }
    }
}